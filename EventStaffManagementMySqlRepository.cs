using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace musicfestivalmanagementapp
{
    internal class EventStaffManagementMySqlRepository : IEventStaffManagementRepository
    {
        private string connectionString;
        private MySql.Data.MySqlClient.MySqlConnection conn;
        public const string BASE_SQL_COMMAND = @"
SELECT p.id AS main_person_id,
p.person_name,
p.person_contact,
p.person_email,
p.person_role,
v.id AS vendor_id, 
v.vendor_company_name,
c.id AS crew_id,
c.hourly_rate,
c.employee_type,
c.weekly_hour,
performers.fee, 
performers.id AS performer_id,
categories.category_name,
genres.genre_name
FROM people p LEFT JOIN vendors v ON v.person_id = p.id 
LEFT JOIN crew c ON c.person_id = p.id
LEFT JOIN performers ON performers.person_id = p.id 
LEFT JOIN genres ON genres.performers_id = performers.id 
LEFT JOIN categories ON categories.vendor_id = v.id;
";
        public EventStaffManagementMySqlRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private MySqlConnection CreateConnection()
        {
            conn = new MySqlConnection();
            conn.ConnectionString = connectionString;
            return conn;
        }
        public void Add(EventStaff eventstaff)
        {
            if (eventstaff == null) throw new ArgumentNullException(nameof(eventstaff));
            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (MySqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        int personId = AddPerson(conn, tx, eventstaff);

                        if(eventstaff is Vendor vendor) { AddVendor(conn, tx, personId, vendor); }
                        else if(eventstaff is Crew crew) { AddCrew(conn, tx, personId, crew); }
                        else if (eventstaff is Performer performer) { AddPerformer(conn, tx, personId, performer); }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }


        }
        public bool EmailExists(string email)
        {
            return false;

        }
        public void Delete(string PersonID) { }
        public List<EventStaff> GetAll() {
            return new List<EventStaff>();

        }
    private int AddPerson(MySqlConnection conn, MySqlTransaction tx, EventStaff eventstaff)
        {
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.Transaction = tx;
                cmd.CommandText = @"INSERT INTO people (person_name, person_contact, person_email, person_role) 
VALUES (@person_name, @person_contact, @person_email,  @role); SELECT LAST_INSERT_ID();";
                cmd.Parameters.AddWithValue("@person_name", eventstaff.Name);
                cmd.Parameters.AddWithValue("@person_contact", eventstaff.Telephone);
                cmd.Parameters.AddWithValue("@person_email", eventstaff.Email);
                cmd.Parameters.AddWithValue("@role", eventstaff.Role);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }

        }
        private void AddVendor(MySqlConnection conn, MySqlTransaction tx, int personId, Vendor vendor)
        {
            int VendorId;
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.Transaction = tx;
                cmd.CommandText = "INSERT INTO vendors(vendor_company_name, person_id) VALUES (@vendor_name, @personID); SELECT LAST_INSERT_ID();";
                cmd.Parameters.AddWithValue("@vendor_name", vendor.VendorName);
                cmd.Parameters.AddWithValue("@personID", personId);
                VendorId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            foreach (string Category_name in vendor.Categories)
            {
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tx;
                    cmd.CommandText = @"INSERT INTO categories(category_name, vendor_id) VALUES (@name, @vendor_id);";
                    cmd.Parameters.AddWithValue("@name", Category_name);
                    cmd.Parameters.AddWithValue("@vendor_id", VendorId);
                    cmd.ExecuteNonQuery();
                }

            }
        }
        private void AddPerformer(MySqlConnection conn, MySqlTransaction tx, int personId, Performer performer)
        {
            int PerformerId;
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.Transaction = tx;
                cmd.CommandText = "INSERT INTO performers(fee, person_id) VALUES (@fee, @personID); SELECT LAST_INSERT_ID();";
                cmd.Parameters.AddWithValue("@fee", performer.Fee);
                cmd.Parameters.AddWithValue("@personID", personId);
                PerformerId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            foreach (string genre_name in performer.Genres)
            {
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tx;
                    cmd.CommandText = @"INSERT INTO genres(genre_name, performer_id) VALUES (@name, @performer_id);";
                    cmd.Parameters.AddWithValue("@name", genre_name);
                    cmd.Parameters.AddWithValue("@performer_id", PerformerId);
                    cmd.ExecuteNonQuery();
                }

            }
        }
        private void AddCrew(MySqlConnection conn, MySqlTransaction tx, int personId, Crew crew)
        {
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.Transaction = tx;
                cmd.CommandText = @"INSERT INTO crew(employee_type, hourly_rate, weekly_hours, person_id)
VALUES (@emptype, @hourly_rate, @weekly_hours, @person_id); ";
                cmd.Parameters.AddWithValue("@emptype", crew.EmploymentType);
                cmd.Parameters.AddWithValue("@hourly_rate", crew.HourlyRate);
                cmd.Parameters.AddWithValue("@weekly_hours", crew.WeeklyHours);
                cmd.Parameters.AddWithValue(@"person_id", personId);
                cmd.ExecuteNonQuery();
            }
        }

    } 
}
