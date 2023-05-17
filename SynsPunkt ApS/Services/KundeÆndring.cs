using SynsPunkt_ApS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    internal class KundeÆndring
    {
        public class CRUDKunde
        {
            // forbindelse til SSMS
            private string connectionString = "Data Source=mssql15.unoeuro.com;Initial Catalog=oversaftigt_dk_db_test;User ID=oversaftigt_dk;Password=prfHFR9546nEtyb2xDmc";

            // Dette er en metode som kan finde en bestemt kunde med et bestemt ID som henter fra Databasen 
            public void HentKundeMedInfo(int KundeInfo)
            {
                // Her anmoder man data fra Databasen for at få kundeoplysninger for det angivne ID
                string query = $"SELECT * FROM Kunder WHERE Id = {KundeInfo}";
                SqlDataReader reader = ExecuteReader(query);
                // Her bruger jeg if-statements til at se om mit udsagn er rigtig. 
                if (reader.HasRows)
                {
                    // While-statement bruges til at koden bliver gentagne gange udført hvis mit udsagn er rigtigt. 
                    while (reader.Read())
                    {
                        Kunde kunde = new Kunde
                     (
                            reader["Fornavn"].ToString(),
                            reader["Efternavn"].ToString(),
                            Convert.ToInt32(reader["TelefonNummer"]),
                            reader["PrivatEmail"].ToString(),
                            reader["Adresse"].ToString(),
                            reader["KundeNummer"].ToString(),
                            reader["KundeInfo"].ToString()
                        );

                        // her bliver kundens oplysninger skrevet på skærmen 
                        Console.WriteLine("Kundens oplysninger:");
                        Console.WriteLine("ID: " + kunde.KundeInfo);
                        Console.WriteLine("Fornavn: " + kunde.Fornavn);
                        Console.WriteLine("Efternavn: " + kunde.Efternavn);
                        Console.WriteLine("Email: " + kunde.PrivatEmail);
                        Console.WriteLine("Telefonnummer: " + kunde.TelefonNummer);
                        Console.WriteLine("Adresse: " + kunde.Adresse);
                    }
                }
                // Else-statement hvis mit udsagn er forkert
                else
                {
                    Console.WriteLine("Kunden blev ikke fundet.");
                }

                reader.Close();
            }

            private SqlDataReader ExecuteReader(string query)
            {
                throw new NotImplementedException();
            }

            // Her er metoden som hvor man kan opdatere kunden 
            public void OpdaterKunde(Kunde opdateretKunde)
            {
                string query = $"UPDATE Kunder SET Fornavn = '{opdateretKunde.Fornavn}', Efternavn = '{opdateretKunde.Efternavn}', Email = '{opdateretKunde.PrivatEmail}', Telefonnummer = '{opdateretKunde.KundeNummer}', Adresse = '{opdateretKunde.Adresse}' WHERE Id = {opdateretKunde.KundeInfo.ToString()}";
                ExecuteQuery(query);
                Console.WriteLine("Kunde opdateret succesfuldt!");
            }

            public void SletKunde(int id)
            {
                string query = $"DELETE FROM Kunder WHERE Id = {id}";
                ExecuteQuery(query);
                Console.WriteLine("Kunde slettet succesfuldt!");
            }

            private void ExecuteQuery(string query)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            private SqlDataReader ExecuteReaderQuery(string query)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                return reader;
            }

        }
    }
}

    

