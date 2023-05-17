using SynsPunkt_ApS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynsPunkt_ApS.Database
{
    public class CRUD_Kunde
    {
        public void CreateKunde()
        {
            // Logik
        }
        public void ReadKunde ()
        {
            // Logik
        }
        public void UpdateKunde()
        {
            // Logik
        }
        public void DeleteKunde()
        {
            // Logik
        }

        internal class KundeÆndring
        {
            public class CRUDKunde
            {
                private string connectionString = "Data Source=mssql15.unoeuro.com;Initial Catalog=oversaftigt_dk_db_test;User ID=oversaftigt_dk;Password=prfHFR9546nEtyb2xDmc";

                public void OpretKunde(Kunde nyKunde)
                {
                    string query = $"INSERT INTO Kunder (Fornavn, Efternavn, TelefonNummer, PrivatEmail, Adresse, KundeNummer, KundeInfo) VALUES ('{nyKunde.Fornavn}', '{nyKunde.Efternavn}', '{nyKunde.TelefonNummer}', '{nyKunde.PrivatEmail}', '{nyKunde.Adresse}', '{nyKunde.KundeNummer}', '{nyKunde.KundeInfo}')";
                    ExecuteQuery(query);
                    MessageBox.Show("Kunde oprettet succesfuldt!");
                }

                public void HentKundeMedInfo(int KundeInfo)
                {
                    string query = $"SELECT * FROM Kunder WHERE Id = {KundeInfo}";
                    SqlDataReader reader = ExecuteReader(query);

                    if (reader.HasRows)
                    {
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

                            MessageBox.Show("Kundens oplysninger:\n" +
                                            "ID: " + kunde.KundeInfo + "\n" +
                                            "Fornavn: " + kunde.Fornavn + "\n" +
                                            "Efternavn: " + kunde.Efternavn + "\n" +
                                            "Email: " + kunde.PrivatEmail + "\n" +
                                            "Telefonnummer: " + kunde.TelefonNummer + "\n" +
                                            "Adresse: " + kunde.Adresse);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kunden blev ikke fundet.");
                    }

                    reader.Close();
                }

                private SqlDataReader ExecuteReader(string query)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            return command.ExecuteReader();
                        }
                    }
                }

                public void OpdaterKunde(Kunde opdateretKunde)
                {
                    string query = $"UPDATE Kunder SET Fornavn = '{opdateretKunde.Fornavn}', Efternavn = '{opdateretKunde.Efternavn}', Email = '{opdateretKunde.PrivatEmail}', Telefonnummer = '{opdateretKunde.KundeNummer}', Adresse = '{opdateretKunde.Adresse}' WHERE Id = {opdateretKunde.KundeInfo.ToString()}";
                    ExecuteQuery(query);
                    MessageBox.Show("Kunde opdateret succesfuldt!");
                }

                public void SletKunde(int id)
                {
                    string query = $"DELETE FROM Kunder WHERE Id = {id}";
                    ExecuteQuery(query);
                    MessageBox.Show("Kunde slettet succesfuldt!");
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
            }
        }
    }

}
