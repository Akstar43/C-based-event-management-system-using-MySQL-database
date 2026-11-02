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
SELECT p.id AS person_id,
p.person_name,
p.person_contact,
p.person_email,
p.person_role,
v.id AS vendor_id, 
v.vendor_company_name,
c.id AS crew_id,
c.hourly_rate,
c.employee_type,
c.weekly_hours,
performers.fee, 
performers.id AS performer_id,
GROUP_CONCAT(DISTINCT categories.category_name) AS categories_list,
GROUP_CONCAT(DISTINCT genres.genre_name) AS genres_list
FROM people p LEFT JOIN vendors v ON v.person_id = p.id 
LEFT JOIN crew c ON c.person_id = p.id
LEFT JOIN performers ON performers.person_id = p.id 
LEFT JOIN genres ON genres.performer_id = performers.id 
LEFT JOIN categories ON categories.vendor_id = v.id GROUP BY p.id;
";
        public const string GET_ALL_VENDORS = @"
SELECT p.id AS person_id,
p.person_name,
p.person_contact,
p.person_email,
p.person_role,
v.id AS vendor_id, 
v.vendor_company_name,
NULL AS crew_id,
NULL AS hourly_rate,
NULL AS employee_type,
NULL AS weekly_hours,
NULL AS performers_fee, 
NULL AS performer_id,
GROUP_CONCAT(DISTINCT categories.category_name) AS categories_list,
NULL AS genre_list
FROM people p LEFT JOIN vendors v ON v.person_id = p.id 
LEFT JOIN crew c ON c.person_id = p.id
LEFT JOIN performers ON performers.person_id = p.id 
LEFT JOIN genres ON genres.performer_id = performers.id 
LEFT JOIN categories ON categories.vendor_id = v.id WHERE p.person_role = 'Vendor' GROUP BY p.id;
";
        public const string GET_ALL_PERFORMERS = @"
SELECT p.id AS person_id,
p.person_name,
p.person_contact,
p.person_email,
p.person_role,
NULL AS vendor_id, 
NULL AS vendor_company_name,
NULL AS crew_id,
NULL AS hourly_rate,
NULL AS employee_type,
NULL AS weekly_hours,
performers.id,
performers.fee, 
performers.id,
GROUP_CONCAT(DISTINCT genres.genre_name) AS genres_list,
NULL AS categories_list
FROM people p LEFT JOIN vendors v ON v.person_id = p.id 
LEFT JOIN crew c ON c.person_id = p.id
LEFT JOIN performers ON performers.person_id = p.id 
LEFT JOIN genres ON genres.performer_id = performers.id 
LEFT JOIN categories ON categories.vendor_id = v.id WHERE p.person_role = 'Performer' GROUP BY p.id;
";
        public const string GET_ALL_CREW = @"
SELECT p.id AS person_id,
p.person_name,
p.person_contact,
p.person_email,
p.person_role,
NULL AS vendor_id, 
NULL AS vendor_company_name,
c.id AS crew_id,
c.hourly_rate,
c.employee_type,
c.weekly_hours,
NULL AS performers_id,
NULL AS performer_fee, 
NULL AS performer_id,
NULL AS genres_list,
NULL AS categories_list
FROM people p LEFT JOIN vendors v ON v.person_id = p.id 
LEFT JOIN crew c ON c.person_id = p.id
LEFT JOIN performers ON performers.person_id = p.id 
LEFT JOIN genres ON genres.performer_id = performers.id 
LEFT JOIN categories ON categories.vendor_id = v.id WHERE p.person_role = 'Crew' GROUP BY p.id;
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
        public void Delete(int PersonID) { 
        using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                string sql = @"DELETE FROM people WHERE id = @id;";
                using(MySqlCommand cmd = new MySqlCommand (sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id",  PersonID);
                    cmd.ExecuteNonQuery();
                }

            }        
                    
        }
        public List<EventStaff> GetAll()
        {
            List<EventStaff> results = new List<EventStaff>();
            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(BASE_SQL_COMMAND, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) {
                        results.Add(Maprowtoitem(reader));
                    }
                }
                return results;
            }
        }
        public List<EventStaff> GetAllVendors()
        {
            List<EventStaff> results = new List<EventStaff>();
            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(GET_ALL_VENDORS, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(Maprowtoitem(reader));
                    }
                }
                return results;
            }
        }
        public List<EventStaff> GetAllPerformers()
        {
            List<EventStaff> results = new List<EventStaff>();
            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(GET_ALL_PERFORMERS, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(Maprowtoitem(reader));
                    }
                }
                return results;
            }
        }
                public List<EventStaff> GetAllCrew()
        {
            List<EventStaff> results = new List<EventStaff>();
            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(GET_ALL_CREW, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(Maprowtoitem(reader));
                    }
                }
                return results;
            }
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
        private EventStaff Maprowtoitem(MySqlDataReader reader) {
            string personId = reader.GetInt32("person_id").ToString();
            string personName = reader.GetString("person_name");
            string personContact = reader.GetString("person_contact");
            string personEmail = reader.GetString("person_email");
            string personRole = reader.GetString("person_role");
            if(personRole == "Vendor")
            {
                string vendorCompanyName = reader.GetString("vendor_company_name");
                string rowcategries = reader.GetString("categories_list");
                List<string> categoriesList = rowcategries.Split(',').ToList();
                return new Vendor(vendorCompanyName, categoriesList, personId, personName, personContact, personRole, personEmail);
             }
            else if (personRole == "Performer")
            {
                string fee = reader.GetString("fee");
                string rowgenres = reader.GetString("genres_list");
                List<string> genresList = rowgenres.Split(",").ToList();
                return new Performer(genresList, fee, personId, personName, personContact, personRole, personEmail);

            }
            if (personRole == "Crew")
            {
                string hourlyRate = reader.GetString("hourly_rate");
                string employeeType = reader.GetString("employee_type");
                string weeklyHours = reader.GetString("weekly_hours");
                return new Crew(hourlyRate, weeklyHours, employeeType, personId, personName, personContact, personRole, personEmail);

            }

            return null;
   
        }

    } 
}
