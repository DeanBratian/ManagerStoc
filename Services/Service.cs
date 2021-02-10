//#define DEV
using ManagerStoc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerStoc.Services
{

    public sealed class Service
    {
#if !DEV
        private SqlConnection DataBaseConnection_SqlCOnnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\Database1.mdf;Integrated Security = True");
#else
        private SqlConnection DataBaseConnection_SqlCOnnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dean\source\repos\ManagerStoc\ManagerStoc\Database1.mdf;Integrated Security = True");
#endif

        //thread safe singleton even if app is ST
        private static Service instance = null;
        private static readonly object padlock = new object();


        public static Service Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Service();
                    }
                    return instance;
                }
            }
        }

        public bool AttemptDatabaseConnection()
        {
            bool retVal;

            try
            {
                DataBaseConnection_SqlCOnnection.Open();

                if (DataBaseConnection_SqlCOnnection.State == ConnectionState.Open)
                {

                    retVal = true;
                }
                else
                {

                    retVal = false;
                }

                DataBaseConnection_SqlCOnnection.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }


            return retVal;
        }

        public bool ExecLoginProcedure(LoginModel LModel)
        {
            bool retVal = false;

            try
            {
                using (SqlCommand Command_SqlCommand = new SqlCommand("LoginProc", DataBaseConnection_SqlCOnnection))
                {
                    Command_SqlCommand.Parameters.AddWithValue("@user", LModel.Username);
                    Command_SqlCommand.Parameters.AddWithValue("@pw", LModel.Parola);
                    Command_SqlCommand.CommandType = CommandType.StoredProcedure;

                    DataBaseConnection_SqlCOnnection.Open();

                    Command_SqlCommand.ExecuteNonQuery();

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(Command_SqlCommand);
                    DataTable DataTable_DT = new DataTable();
                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);
                    DataBaseConnection_SqlCOnnection.Close();

                    if (DataTable_DT.Rows.Count > 0)
                    {
                        retVal = true;
                    }
                    else
                    {
                        retVal = false;
                    }


                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                retVal = false;

            }

            return retVal;

        }



        public bool ExecRegisterProcedure(LoginModel LModel)
        {
            bool retVal = false;

            try
            {
                using (SqlCommand Command_SqlCommand = new SqlCommand("SaveNewAccountProc", DataBaseConnection_SqlCOnnection))
                {
                    Command_SqlCommand.Parameters.AddWithValue("@user", LModel.Username);
                    Command_SqlCommand.Parameters.AddWithValue("@pw", LModel.Parola);


                    Command_SqlCommand.CommandType = CommandType.StoredProcedure;
                    DataBaseConnection_SqlCOnnection.Open();
                    Command_SqlCommand.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                retVal = false;
            }

            return retVal;
        }


        public DataTable ExecuteSelectAllProduseProcedure()
        {

            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("SelectAllProduseProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();
                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {

                        DataTable_retVal = DataTable_DT;
                    }
                    else
                    {

                    }
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            return DataTable_retVal;

        }
        public bool ExecuteUpdateProdusProcedure(ProdusModel PModel)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("EditProdusProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputnume", PModel.NumeProdus);
                    SqlCommand_SC.Parameters.AddWithValue("@inputdescriere", PModel.DescriereProdus);
                    SqlCommand_SC.Parameters.AddWithValue("@inputpretc", PModel.PretCumparare);
                    SqlCommand_SC.Parameters.AddWithValue("@inputpretv", PModel.PretVanzare);
                    SqlCommand_SC.Parameters.AddWithValue("@inputunitatemasura", PModel.UnitateMasura);
                    SqlCommand_SC.Parameters.AddWithValue("@inputid", PModel.IdProdus);

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }


            return retVal;
        }

        public bool ExecuteDeleteProdusProcedure(int IdProdus)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("DeleteProdusProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputid", IdProdus);


                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }

            catch (SqlException ex) when (ex.Number == 547)
            {
                retVal = false;
                DataBaseConnection_SqlCOnnection.Close();
            }


            return retVal;

        }



        public DataTable ExecuteCheckIfImageExistsForProductProcedure(int IdProdus)
        {
            DataTable DataTable_retVal = new DataTable();
            try
            {
                using (SqlCommand SqlCommand_SC = new SqlCommand("CheckIfImageExistsForProductProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputidprodus", IdProdus);

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();

                    DataAdapeter_SqlDataAdapter.Fill(DataTable_retVal);
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataTable_retVal;
        }


        public bool ExecuteInsertImagineProdusProcedure(ImagineProdusModel IPModel, int IdProdus)
        {

            bool retVal;

            try
            {
                using (SqlCommand SqlCommand_SC2 = new SqlCommand("InsertImagineProdusProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC2.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC2.Parameters.AddWithValue("@inputnumefisier", IPModel.FileName);
                    SqlCommand_SC2.Parameters.AddWithValue("@inputimagine", IPModel.Content);
                    SqlCommand_SC2.Parameters.AddWithValue("@inputidprodus", IdProdus);
                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC2.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();
                    retVal = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }

            return retVal;
        }


        public DataTable ExecuteGetNumeFurnizorProcedure()
        {
            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("GetNumeFurnizori", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;

                    }
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataTable_retVal;

        }


        public bool ExecuteAdaugaProdusProcedure(ProdusModel PModel, int IdFurnizor)
        {

            bool retVal;
            try
            {
                using (SqlCommand SqlCommand_SC = new SqlCommand("InsertProdusProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputnume", PModel.NumeProdus);
                    SqlCommand_SC.Parameters.AddWithValue("@inputdescriere", PModel.DescriereProdus);
                    SqlCommand_SC.Parameters.AddWithValue("@inputpretc", PModel.PretCumparare);
                    SqlCommand_SC.Parameters.AddWithValue("@inputpretv", PModel.PretVanzare);
                    SqlCommand_SC.Parameters.AddWithValue("@inputunitatemasura", PModel.UnitateMasura);
                    SqlCommand_SC.Parameters.AddWithValue("@inputidfurnizor", IdFurnizor);

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }


            return retVal;
        }


        public DataTable ExecuteProduseGraphProcedure()
        {
            DataTable retVal = new DataTable();
            try
            {
                using (SqlCommand SqlCommand_SC = new SqlCommand("SelectProduseGraphProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    DataAdapter_SqlDataAdapter.Fill(DataTable_DT);

                    if (DataTable_DT.Rows.Count > 0)
                    {
                        retVal = DataTable_DT;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return retVal;
        }


        public DataTable ExecuteSelectAllServiciiProcedure()
        {
            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("SelectAllServiciiProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();
                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);

                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;
                    }
                    else
                    {

                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            return DataTable_retVal;
        }



        public bool ExecuteUpdateServiciuProcedure(ServiciuModel SModel)
        {

            bool retVal;
            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("EditServiciuProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputnumeserviciu", SModel.NumeServiciu);
                    SqlCommand_SC.Parameters.AddWithValue("@inputdescriereserviciu", SModel.DescriereServiciu);
                    SqlCommand_SC.Parameters.AddWithValue("@inputpret", SModel.Pret);
                    SqlCommand_SC.Parameters.AddWithValue("@inputdurata", SModel.Durata);
                    SqlCommand_SC.Parameters.AddWithValue("@inputid", SModel.IdServiciu);

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();
                    retVal = true;

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }


            return retVal;
        }

        public bool ExecuteDeleteServiciuProcedure(int IdServiciu)
        {
            bool retVal;
            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("DeleteServiciuProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputid", IdServiciu);


                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }

            catch (SqlException ex) when (ex.Number == 547)
            {
                retVal = false;
                DataBaseConnection_SqlCOnnection.Close();
            }


            return retVal;
        }

        public bool ExecuteAdaugaServiciuProcedure(ServiciuModel SModel)
        {
            bool retVal;
            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("InsertServiciuProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputnumeserviciu", SModel.NumeServiciu);
                    SqlCommand_SC.Parameters.AddWithValue("@inputdescriereserviciu", SModel.DescriereServiciu);
                    SqlCommand_SC.Parameters.AddWithValue("@inputpret", SModel.Pret);
                    SqlCommand_SC.Parameters.AddWithValue("@inputdurata", SModel.Durata);
                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }

            return retVal;
        }


        public DataTable ExecuteServiciiGraphProcedure()
        {
            DataTable retVal = new DataTable();
            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("SelectServiciiGraphProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    DataAdapter_SqlDataAdapter.Fill(DataTable_DT);

                    if (DataTable_DT.Rows.Count > 0)
                    {
                        retVal = DataTable_DT;
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            return retVal;

        }

        public DataTable ExecuteSelectImaginiProduseProcedure()
        {

            DataTable retVal = new DataTable();
            try
            {
                using (SqlCommand SqlCommand_SC = new SqlCommand("SelectProduseImaginiProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        retVal = DataTable_DT;

                    }
                    else
                    {

                    }

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return retVal;
        }

        public bool ExecuteDeleteImagineProducedure(int IdImagine)
        {
            bool retVal;

            try
            {
                using (SqlCommand SqlCommand_SC = new SqlCommand("DeleteImagineProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputid", IdImagine);


                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();
                    retVal = true;

                }

            }

            catch (SqlException ex) when (ex.Number == 547)
            {
                retVal = false;
                DataBaseConnection_SqlCOnnection.Close();
            }


            return retVal;
        }
        public DataTable ExecuteSelectIntrariProducedure()
        {
            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("SelectAllIntrariProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();
                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;
                    }
                    else
                    {

                    }
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataTable_retVal;
        }


        public bool ExecuteDeleteIntrareProcedure(int IdIntrare)
        {
            bool retVal;
            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("DeleteIntrareProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputid", IdIntrare);

                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }

            catch (SqlException ex) when (ex.Number == 547)
            {
                retVal = false;
                DataBaseConnection_SqlCOnnection.Close();
            }

            return retVal;

        }


        public DataTable ExecuteGetProdusCantitateDeModificat(int IdIntrare)
        {
            DataTable DataTable_retVal = new DataTable();

            try
            {
                using (SqlCommand SqlCommand_SC = new SqlCommand("GetProdusCantitateDeModificatProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputidintrare", IdIntrare);

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);

                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;

                    }
                    else
                    {

                    }

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataTable_retVal;

        }

        public bool ExecuteCresteStocProcedure(ProdusDeUpdatatInStocModel PDUModel)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("CrestereStocProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputidprodus", PDUModel.IdProdus);
                    SqlCommand_SC.Parameters.AddWithValue("@inputcantitate", PDUModel.Cantitate);


                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }

            return retVal;

        }


        public bool ExecuteScadeStocProcedure(ProdusDeUpdatatInStocModel PDUModel)
        {
            bool retVal;
            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("ScadereStocProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputidprodus", PDUModel.IdProdus);
                    SqlCommand_SC.Parameters.AddWithValue("@inputcantitate", PDUModel.Cantitate);

                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();
                    retVal = true;

                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }

            return retVal;
        }


        public bool ExecuteUndeleteIntrareProcedure(int IdIntrare)
        {
            bool retVal;
            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("UndeleteIntrareProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputid", IdIntrare);


                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();
                    retVal = true;
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }

            return retVal;
        }


        public DataTable ExecuteGetNumeFurnizoriPentruProdusProcedure(int IdProdusAles)
        {
            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("GetNumeFurnizoriPentruProdus", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;


                    SqlCommand_SC.Parameters.AddWithValue("@inputidprodus", IdProdusAles);

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;

                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataTable_retVal;
        }



        public DataTable ExecuteGetNumeProduseProcedure()
        {

            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("GetNumeProduse", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;

                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            return DataTable_retVal;
        }

        public bool ExecuteAdaugaIntrareProcedure(IntrareModel IModel)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("InsertIntrareProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputidprodus", IModel.IdProdus);
                    SqlCommand_SC.Parameters.AddWithValue("@inputcantitate", IModel.Cantitate);
                    SqlCommand_SC.Parameters.AddWithValue("@inputidfurnizor", IModel.IdFurnizor);
                    SqlCommand_SC.Parameters.AddWithValue("@inputpretc", IModel.PretCumparare);

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();
                    retVal = true;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }

            return retVal;


        }


        public DataTable ExecuteSelectAllClientiProcedure()
        {

            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("SelectAllClientiProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();
                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;
                    }

                    else
                    {

                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            return DataTable_retVal;

        }


        public bool ExecuteUpdateClientProcedure(ClientModel CModel)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("EditClientProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputnume", CModel.NumeClient);
                    SqlCommand_SC.Parameters.AddWithValue("@inputdescriere", CModel.DescriereClient);
                    SqlCommand_SC.Parameters.AddWithValue("@inputcodfiscal", CModel.CodFiscal);
                    SqlCommand_SC.Parameters.AddWithValue("@inputid", CModel.IdClient);

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();
                    retVal = true;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }

            return retVal;

        }


        public bool ExecuteDeleteClientProcedure(int IdClient)
        {
            bool retVal;
            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("DeleteClientProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputid", IdClient);

                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();
                    retVal = true;
                }



            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                retVal = false;
                DataBaseConnection_SqlCOnnection.Close();
            }


            return retVal;
        }


        public bool ExecuteInsertClientProcedure(ClientModel CModel)
        {
            bool retVal;

            try
            {
                using (SqlCommand SqlCommand_SC = new SqlCommand("InsertClientProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputnume", CModel.NumeClient);
                    SqlCommand_SC.Parameters.AddWithValue("@inputdescriere", CModel.DescriereClient);
                    SqlCommand_SC.Parameters.AddWithValue("@inputcodfiscal", CModel.CodFiscal);


                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }

            return retVal;
        }


        public DataTable ExecuteGetNumeClienti()
        {

            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("GetNumeClienti", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();
                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;

                    }
                    else
                    {

                    }
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            return DataTable_retVal;
        }


        public DataTable ExecuteGetVanzariForClientInPerioada(DataModel DModel, int IdClient)
        {

            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("SelectAllVanzariFromClientInPerioadaProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;


                    SqlCommand_SC.Parameters.AddWithValue("@inputidclient", IdClient);
                    SqlCommand_SC.Parameters.AddWithValue("@inputdatainceput", DModel.DataInceput);
                    SqlCommand_SC.Parameters.AddWithValue("@inputdatasfarsit", DModel.DataSfarsit);

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();
                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;
                    }
                    else
                    {


                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataTable_retVal;
        }


        public bool ExecuteInsertNumarMasinaProcedure(String NumarMasina)
        {
            bool retVal;
            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("InsertNrMasinaProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputnrmasina", NumarMasina);

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }

            return retVal;
        }

        public DataTable ExecuteSelectNumereMasinaProcedure()
        {

            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("SelectAllNumereMasinaProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();
                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;

                    }

                    else
                    {

                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataTable_retVal;

        }

        public bool ExecuteUpdateNumarProcedure(NumarMasinaModel NMModel)
        {

            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("EditNumarMasinaProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;
                    SqlCommand_SC.Parameters.AddWithValue("@inputnrmasina", NMModel.NumarMasina);
                    SqlCommand_SC.Parameters.AddWithValue("@inputid", NMModel.IdNumar);

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }

            return retVal;
        }


        public bool ExecuteDeleteNumarProcedure(int IdNumar)
        {
            bool retVal;
            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("DeleteNumarMasinaProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputid", IdNumar);

                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }

            catch (SqlException ex) when (ex.Number == 547)
            {
                DataBaseConnection_SqlCOnnection.Close();

                retVal = false;
            }

            return retVal;
        }
    
    
    
    
        public DataTable ExecuteGetContracteProcedure()
        {
            DataTable DataTable_retVal = new DataTable();
            
            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("GetNumeContracte", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;

                    }
                    else
                    {


                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataTable_retVal;
        }
    
    
    
        public bool ExecuteInsertPretTotalProcedure(PretTotalModel PTModel, int IdContract)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("InsertPretSTProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputnumest", PTModel.NumeServiciuTotal);
                    SqlCommand_SC.Parameters.AddWithValue("@inputdetalii", PTModel.Detalii);
                    SqlCommand_SC.Parameters.AddWithValue("@inputpret", PTModel.Pret);
                    SqlCommand_SC.Parameters.AddWithValue("@inputidcontract", IdContract);

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                retVal = false;
            }

            return retVal;
        }


        public DataTable ExecuteSelectAllPreturiProcedure()
        {

            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("SelectAllPreturiSTProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();
                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;

                    }
                    else
                    {

                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataTable_retVal;
        }


        public bool ExecuteUpdatePretProcedure(PretTotalModel PTModel)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("EditPretSTProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputnume", PTModel.NumeServiciuTotal);
                    SqlCommand_SC.Parameters.AddWithValue("@inputdetalii", PTModel.Detalii);
                    SqlCommand_SC.Parameters.AddWithValue("@inputpret", PTModel.Pret);
                    SqlCommand_SC.Parameters.AddWithValue("@inputid", PTModel.IdPretTotal);

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }

            return retVal;

        }


        public bool ExecuteDeletePretProcedure(int IdPretTotal)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("DeletePretSTProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputid", IdPretTotal);

                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }

            catch (SqlException ex) when (ex.Number == 547)
            {
                DataBaseConnection_SqlCOnnection.Close();
                retVal = false;
            }

            return retVal;
        }


        public bool ExecuteInsertFurnizorProcedure(FurnizorModel FModel)
        {

            bool retVal;
            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("InsertFurnizorProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputnumefurnizor", FModel.NumeFurnizor);
                    SqlCommand_SC.Parameters.AddWithValue("@inputdetalii", FModel.Detalii);

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();
                    retVal = true;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }

            return retVal;
        }


        public DataTable ExecuteSelectAllFurnizoriProcedure()
        {

            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("SelectAllFurnizoriProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();
                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;

                    }

                    else
                    {

                      
                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataTable_retVal;
        }



        public bool ExecuteUpdateFurnizorProcedure(FurnizorModel FModel)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("EditFurnizorProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;
                    SqlCommand_SC.Parameters.AddWithValue("@inputnumefurnizor", FModel.NumeFurnizor);
                    SqlCommand_SC.Parameters.AddWithValue("@inputdetalii", FModel.Detalii);
                    SqlCommand_SC.Parameters.AddWithValue("@inputid", FModel.IdFurnizor);

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }

            return retVal;
        }

        public bool ExecuteDeleteFurnizorProcedure(int IdFurnizor)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("DeleteFurnizorProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputid", IdFurnizor);


                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }


            }

            catch (SqlException ex) when (ex.Number == 547)
            {
                DataBaseConnection_SqlCOnnection.Close();
                retVal = false;
            }

            return retVal;
        }


        public DataTable ExecuteGetNumeClientiProcedure()
        {
            DataTable DataTable_retVal = new DataTable();
            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("GetNumeClienti", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;

                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataTable_retVal;

        }

        public bool ExecuteInsertContractProcedure(ContractModel CModel, int IdClient)
        {

            bool retVal;
            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("InsertContractProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputdetalii", CModel.DetaliiContract);
                    SqlCommand_SC.Parameters.AddWithValue("@inputdurata", CModel.Durata);
                    SqlCommand_SC.Parameters.AddWithValue("@inputidclient", IdClient);
                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();
                    retVal = true;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }


            return retVal;

        }


        public DataTable ExecuteSelectAllContracteProcedure()
        {

            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("SelectAllContracteProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();
                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;

                    }
                    else
                    {

                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataTable_retVal;
        }


        public bool ExecuteUpdateContractProcedure(ContractModel CModel)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("EditContractProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputdetalii", CModel.DetaliiContract);
                    SqlCommand_SC.Parameters.AddWithValue("@inputdurata", CModel.Durata);
                    SqlCommand_SC.Parameters.AddWithValue("@inputid", CModel.IdContract);

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();
                    retVal = true;
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }

            return retVal;
        }



        public bool ExecuteDeleteContractProcedure(int IdContract)
        {
            bool retVal;
            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("DeleteContractProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputid", IdContract);


                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }

            catch (SqlException ex) when (ex.Number == 547)
            {
                retVal = false; 
                DataBaseConnection_SqlCOnnection.Close();
            }


            return retVal;
        }


        public DataTable ExecuteGetNumeContracteProcedure()
        {
            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("GetNumeContracte", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();
                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;

                    }
                    else
                    {

                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataTable_retVal;
        }



        public bool ExecuteInsertFacturaProcedure(FacturaModel FModel, int IdClient, int IdContract, DateTime DataScadenta)
        {

            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("InsertFacturaProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputdatascadenta", DataScadenta);
                    SqlCommand_SC.Parameters.AddWithValue("@inputfacturaachitata", FModel.FacturaAchitata);
                    SqlCommand_SC.Parameters.AddWithValue("@inputsumatotala", FModel.SumaTotala);
                    SqlCommand_SC.Parameters.AddWithValue("@inputidcontract", IdContract);
                    SqlCommand_SC.Parameters.AddWithValue("@inputidclient", IdClient);
                    SqlCommand_SC.Parameters.AddWithValue("@inputdirectiefactura", FModel.DirectieFactura);


                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;
            }

            return retVal;
        }


        public DataTable ExecuteSelectAllFacturiProcedure()
        {

            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("SelectAllFacturiProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();
                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;

                    }
                    else
                    {

                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataTable_retVal;
        }

        public bool ExecuteDeleteFacturaProcedure(int IdFactura)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("DeleteFacturaProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputid", IdFactura);


                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();
                    
                    retVal = true;
                }

            }

            catch (SqlException ex) when (ex.Number == 547)
            {
                retVal = false;
                DataBaseConnection_SqlCOnnection.Close();
            }
            
            
            return retVal;

        }


        public bool ExecuteUpdateFacturaProcedure(FacturaModel FModel)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("EditFacturaProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputachitat", FModel.FacturaAchitata);
                    SqlCommand_SC.Parameters.AddWithValue("@inputid", FModel.IdFactura);


                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retVal = false;

            }

            return retVal;
        }


        public DataTable ExecuteSelectAllVanzariProcedure()
        {

            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("SelectAllVanzariProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();
                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;

                    }
                    else
                    {

                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataTable_retVal;
        }


        public bool ExecuteDeleteVanzareProcedure(int IdVanzare)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("DeleteVanzareProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputid", IdVanzare);


                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }



            }

            catch (SqlException ex) when (ex.Number == 547)
            {
                DataBaseConnection_SqlCOnnection.Close();
                retVal = false;
            }

            return retVal;
        }


        public DataTable ExecuteCheckIfRelatedVanzariProduseExistProcedure(int IdVanzare)
        {

            DataTable DataTable_retVal = new DataTable();

            try
            {
                using (SqlCommand SqlCommand_SC = new SqlCommand("CheckIfRelatedVanzariProduseExistProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputidvanzare", IdVanzare);

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);

                    if (DataTable_DT.Rows.Count > 0)
                    {

                        DataTable_retVal = DataTable_DT;

                    }
                    else
                    {

                    }

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            return DataTable_retVal;
        }

        public bool ExecuteDeleteVanzareProdusFromDeleteVanzareProcedure(int IdVanzare)
        {
            bool retVal;


            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("DeleteVanzareProdusFromDeleteVanzareProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputid", IdVanzare);


                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }


            }

            catch (SqlException ex) when (ex.Number == 547)
            {
                DataBaseConnection_SqlCOnnection.Close();
                retVal = false;
            }

            return retVal;
        }


        public DataTable ExecuteCheckIfRelatedVanzariServiciiExistProcedure(int IdVanzare)
        {

            DataTable DataTable_retVal = new DataTable();

            try
            {
                using (SqlCommand SqlCommand_SC = new SqlCommand("CheckIfRelatedVanzariServiciiExistProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputidvanzare", IdVanzare);

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);

                    if (DataTable_DT.Rows.Count > 0)
                    {

                        DataTable_retVal = DataTable_DT;

                    }
                    else
                    {

                    }

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            return DataTable_retVal;
        }


        public bool ExecuteDeleteVanzareServiciuFromDeleteVanzareProcedure(int IdVanzare)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("DeleteVanzareServiciuFromDeleteVanzareProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputid", IdVanzare);


                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }


            }

            catch (SqlException ex) when (ex.Number == 547)
            {
                DataBaseConnection_SqlCOnnection.Close();
                retVal = false;
            }
            return retVal;
        }


        public bool ExecuteUndeleteVanzareProcedure(int IdVanzare)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("UndeleteVanzareProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputid", IdVanzare);


                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }

            }

            catch (SqlException ex) when (ex.Number == 547)
            {
                DataBaseConnection_SqlCOnnection.Close();
                retVal = false;
            }
            return retVal;
        }


        public bool ExecuteUnDeleteVanzareProdusFromUnDeleteVanzareProcedure(int IdVanzare)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("UndeleteVanzareProdusFromUndeleteVanzareProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputid", IdVanzare);


                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }


            }

            catch (SqlException ex) when (ex.Number == 547)
            {
                retVal = false;
                DataBaseConnection_SqlCOnnection.Close();
            }

            return retVal;
        }


        public bool ExecuteUnDeleteVanzareServiciuFromUnDeleteVanzareProcedure(int IdVanzare)
        {
            bool retVal;

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("UndeleteVanzareServiciuFromUndeleteVanzareProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlCommand_SC.Parameters.AddWithValue("@inputid", IdVanzare);


                    DataBaseConnection_SqlCOnnection.Open();

                    SqlCommand_SC.ExecuteNonQuery();

                    DataBaseConnection_SqlCOnnection.Close();

                    retVal = true;
                }


            }

            catch (SqlException ex) when (ex.Number == 547)
            {
                retVal = false;
                DataBaseConnection_SqlCOnnection.Close();
            }

            return retVal;
        }



        public DataTable ExecuteSelectAllVanzariServiciiProcedure()
        {

            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("SelectAllVanzariServiciiProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();
                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;

                    }
                    else
                    {

                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            return DataTable_retVal;
        }


        public DataTable ExecuteSelectAllVanzariProduseProcedure()
        {

            DataTable DataTable_retVal = new DataTable();

            try
            {

                using (SqlCommand SqlCommand_SC = new SqlCommand("SelectAllVanzariProduseProc", DataBaseConnection_SqlCOnnection))
                {
                    SqlCommand_SC.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DataAdapeter_SqlDataAdapter = new SqlDataAdapter(SqlCommand_SC);

                    DataTable DataTable_DT = new DataTable();

                    DataBaseConnection_SqlCOnnection.Open();
                    SqlCommand_SC.ExecuteNonQuery();
                    DataBaseConnection_SqlCOnnection.Close();
                    DataAdapeter_SqlDataAdapter.Fill(DataTable_DT);


                    if (DataTable_DT.Rows.Count > 0)
                    {
                        DataTable_retVal = DataTable_DT;
                    }
                    else
                    {

                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return DataTable_retVal;

        }
    }


}
