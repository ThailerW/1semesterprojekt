﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SynsPunkt_ApS.Database
{
    public class CRUD_Vare
    {
        string connectionString = Database.ConnectionString.GetConnectionString();

        /// <summary>
        /// Martin: Opretter en ny vare med alt tildelt info
        /// </summary>
        /// <param name="vareBeskrivelse"></param>
        /// <param name="lagerMængde"></param>
        /// <param name="vareNavn"></param>
        /// <param name="styrke"></param>
        /// <param name="levCVR"></param>
        public void CreateVare(string vareBeskrivelse, int lagerMængde, string vareNavn, decimal styrke, string levCVR)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            try
            {
                command.CommandText = "INSERT INTO SP_Vare (vareBeskrivelse, lagerMængde, vareNavn, styrke, leverandørCVR) " +
                                      "VALUES ('" + vareBeskrivelse + "', " + lagerMængde + ", '" + vareNavn + "', " 
                                      + styrke + ",'" + levCVR + "')";


                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl: " + ex.Message, "OOPS!", MessageBoxButtons.OK);
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }
        }

        /// <summary>
        /// Læser al info knyttet til det indtastede vareID
        /// </summary>
        /// <param name="vareID"></param>
        /// <returns></returns>
        public Models.Vare ReadVare(string id, out string id2, out string vareBeskrivelse, out string lagerMængde, out string vareNavn,
            out string styrke, out string levCVR)
        {
            id2 = "";
            vareBeskrivelse = "";
            lagerMængde = "";
            vareNavn = "";
            styrke = "";
            levCVR = "";

            Models.Vare vare = new Models.Vare(0, "", 0, "", 0, "");
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM SP_Vare WHERE vareID = '" + id + "'";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = null;
            try
            {
                command.CommandText = query;

                connection.Open();
                reader = command.ExecuteReader();

                //While the reader is reading through all the rows, it adds the attributes to the instance of the class.
                while (reader.Read())
                {
                    //Filling created instance with the selected ID's data.
                    vare = new Models.Vare(Convert.ToInt32(reader["vareID"]), reader["vareBeskrivelse"].ToString(),
                       Convert.ToInt32(reader["lagerMængde"]), reader["vareNavn"].ToString(), Convert.ToDecimal(reader["styrke"]),
                       reader["leverandørCVR"].ToString());

                    id2 = vare.VareNummer.ToString();
                    vareBeskrivelse = vare.VareBeskrivelse;
                    lagerMængde = vare.LagerMængde.ToString();
                    vareNavn = vare.VareNavn;
                    styrke = vare.Styrke.ToString();
                    levCVR = vare.LevCVR.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl: " + ex.Message, "OOPS!", MessageBoxButtons.OK);
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }
            return vare;
        }

        /// <summary>
        /// Opdatere en vare med valgt VareID med ny anvgivet data.
        /// </summary>
        /// <param name="vareID"></param>
        /// <param name="vareBeskrivelse"></param>
        /// <param name="lagerMængde"></param>
        /// <param name="vareNavn"></param>
        /// <param name="styrke"></param>
        /// <param name="levCVR"></param>
        public void UpdateVare(string vareID, string vareBeskrivelse, int lagerMængde, string vareNavn, decimal styrke, string levCVR)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            try
            {
                command.CommandText = "UPDATE SP_Vare SET vareBeskrivelse = '" + vareBeskrivelse + "', lagerMængde = " + lagerMængde +
                    ", vareNavn = '" + vareNavn + "', styrke = " + styrke + ", leverandørCVR = " + levCVR + " " +
                    "WHERE vareID = " + vareID + ";";


                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl: " + ex.Message, "OOPS!", MessageBoxButtons.OK);
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }
        }

        /// <summary>
        /// Sletter valgt vare med inputted ID
        /// </summary>
        /// <param name="vareID"></param>
        public void DeleteVare(string vareID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            try
            {
                command.CommandText = "DELETE FROM SP_Vare WHERE vareID = '" + vareID + "'";

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl: " + ex.Message, "OOPS!", MessageBoxButtons.OK);
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }
        }

        /// <summary>
        /// Henter alle vare fra databasen og returnere en liste af models.vare
        /// </summary>
        /// <returns></returns>
        public List<Models.Vare> GetAllVare()
        {
            List<Models.Vare> vareList = new List<Models.Vare>();

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            SqlDataReader reader = null;

            try
            {
                command.CommandText = "SELECT * FROM SP_Vare";

                connection.Open();
                reader = command.ExecuteReader();

                //While the reader is reading through all the rows, it adds the attributes to the instance of the class.
                while (reader.Read())
                {
                    //Creating new instance of class:
                    Models.Vare vare = new Models.Vare(
                       Convert.ToInt32(reader["vareID"]), 
                       reader["vareBeskrivelse"].ToString(),
                       Convert.ToInt32(reader["lagerMængde"]),
                       reader["vareNavn"].ToString(),
                       Convert.ToDecimal(reader["styrke"]),
                       reader["leverandørCVR"].ToString());

                    vareList.Add(vare);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl: " + ex.Message, "OOPS!", MessageBoxButtons.OK);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.Close();
            }
            return vareList;
        }
    }
}
