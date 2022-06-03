using FireBird.SQLHelper;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApiFierbird.Models
{
    public class EskanerIrakurriaDB
    {
        ConnectionConfiguration _connectionConfiguration = new ConnectionConfiguration();
        FbConnection _objcon;
        FbTransaction _objTrans;
        internal DataTable GetAlbaran(string albaran)
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            
            try
            {
                _objcon = _connectionConfiguration.ConnectionString;
                _objcon.Open();
                string query = string.Empty;
                //query = "select distinct trabajos.CODIGO,trabajos.PLAZO,trabajos.MATERIAL,trabajos.TIEMPOPREV,trabajos.FORMATO,";
                //query += " trabajos.F_CORTE_PREV,trabajos.TURNOPREV,trabajos.PREPARACION_MP,trabajos.ESTADO ";
                //query += "from trabajos left outer join Estanteria on Trabajos.CodAlmacen = Estanteria.CodAlmacen and ";
                //query += "Trabajos.Material = Estanteria.CodChapa and Trabajos.Formato = Estanteria.Formato and estanteria.activa = 1 ";
                //query += "where trabajos.maquina = " + maquina + " and trabajos.estado < 4 ";
                //query += "order by trabajos.plazo";
                //query = "select * from ERREZEPZIOA where ALBARAN='"+maquina+"'";


                query = "select DISTINCT ERREZEPZIOA.*, errezepzioabulto.idbulto codbulto from ERREZEPZIOA" +
                   " left outer join errezepzioabulto on " +
                   " (errezepzioa.albaran=errezepzioabulto.albaran and errezepzioa.subcon=errezepzioabulto.subcon and errezepzioa.possubcon=errezepzioabulto.possubcon)" +
                   " where ERREZEPZIOA.ALBARAN='" + albaran + "'" +
                   " and errezepzioabulto.idbulto is null" +
                   " order by ERREZEPZIOA.possubcon";



                ds = SqlFbHelper.ExecuteDataset(_objcon, CommandType.Text, query);
            }
            catch (Exception ex)
            {
                //Log log = new Log();
                //log.WriteLog("GetAlbaran - " + ex.Message.ToString());
                throw (ex);
                //ds = null;
            }
            finally
            {
                if (_objcon.State == ConnectionState.Open)
                {
                    _objcon.Close();
                }
            }
            if (ds == null)
                return dt;
            else
                return ds.Tables[0];

        }

        internal string ExistAlbRegister(string albaran)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            try
            {
                _objcon = _connectionConfiguration.ConnectionString;
                _objcon.Open();
                string query = string.Empty;
                query = "select count(*) from RECEP_SUBCON " +
                    "where N_ALBARAN_COMPRA='" + albaran + "'";



                ds = SqlFbHelper.ExecuteDataset(_objcon, CommandType.Text, query);
            }
            catch (Exception ex)
            {
                //Log log = new Log();
                //log.WriteLog("GetAlbaran - " + ex.Message.ToString());
                throw (ex);
                //ds = null;
            }
            finally
            {
                if (_objcon.State == ConnectionState.Open)
                {
                    _objcon.Close();
                }
            }
            
                return ds.Tables[0].Rows[0][0].ToString();

            
        }

        internal string ExistBulto(int bultoId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            try
            {
                _objcon = _connectionConfiguration.ConnectionString;
                _objcon.Open();
                string query = string.Empty;
                query = "select ID_BULTO from RECEP_SUBCON_BULTO_QR " +
                                   "where ID_BULTO=" + bultoId;



                ds = SqlFbHelper.ExecuteDataset(_objcon, CommandType.Text, query);
            }
            catch (Exception ex)
            {
                //Log log = new Log();
                //log.WriteLog("GetAlbaran - " + ex.Message.ToString());
                throw (ex);
                //ds = null;
            }
            finally
            {
                if (_objcon.State == ConnectionState.Open)
                {
                    _objcon.Close();
                }
            }
            if (ds == null)
                return "false";
            else
            {
                if (ds.Tables[0].Rows.Count == 0)
                    return "false";
                else
                    return "true";

            }
        }

        internal string ExistAlbaran(int codigoBerria)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            try
            {
                _objcon = _connectionConfiguration.ConnectionString;
                _objcon.Open();
                string query = string.Empty;
                query = "select N_ALBARAN_COMPRA from RECEP_SUBCON " +
                                   "where ID_ETIQUETA=" + codigoBerria;



                ds = SqlFbHelper.ExecuteDataset(_objcon, CommandType.Text, query);
            }
            catch (Exception ex)
            {
                //Log log = new Log();
                //log.WriteLog("GetAlbaran - " + ex.Message.ToString());
                throw (ex);
                //ds = null;
            }
            finally
            {
                if (_objcon.State == ConnectionState.Open)
                {
                    _objcon.Close();
                }
            }
            if (ds == null)
                return "false";
            else
            {
                if (ds.Tables[0].Rows.Count == 0)
                    return "false";
                else
                    return "true";

            }
                
        }

        internal ArrayList checkSubcon(ArrayList checkSubconList)
        {
            ArrayList listSubconCheked = new ArrayList();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            try
            {
                _objcon = _connectionConfiguration.ConnectionString;
                _objcon.Open();
                foreach (string subconPedPos in checkSubconList)
                {
                    String[] strlist = subconPedPos.Split(',');
                    string query = string.Empty;
                    query = "select CODIGO,PEDIDO,POS, COALESCE (CANT,0) as CANT, COALESCE (RECEP,0) as RECEP" +
                        ",(select COALESCE (SUM( CANTIDAD_REAL),0) from RECEP_SUBCON_BULTO where SUBCON=LN.CODIGO and PEDIDO=LN.PEDIDO and O_PED=LN.O_PED) AS CANTIDAD_REAL " +
                        " from LNSUBCONT LN where codigo=" + Convert.ToInt32( strlist[0])+
                        " and PEDIDO="+ Convert.ToInt32(strlist[1]) +
                        " and O_PED='" + strlist[2]+"'";



                    ds = SqlFbHelper.ExecuteDataset(_objcon, CommandType.Text, query);
              
                    foreach (DataRow dtrow in ds.Tables[0].Rows)
                    {
                        listSubconCheked.Add("SU:"+dtrow[0].ToString()+"-PE:"+ dtrow[1].ToString() + "-PO:"
                            + dtrow[2].ToString() + "-C:" + dtrow[3].ToString() + "-RECEP:"
                            + dtrow[4].ToString() + "-CREAL:" + dtrow[5].ToString()
                            );
                    }



                }

            }
            catch (Exception ex)
            {
                //Log log = new Log();
                //log.WriteLog("GetAlbaran - " + ex.Message.ToString());
                throw (ex);
                //ds = null;
            }
            finally
            {
                if (_objcon.State == ConnectionState.Open)
                {
                    _objcon.Close();
                }
            }
            //if (ds == null)
            //    return dt;
            //else
            //    return ds.Tables[0];


            return listSubconCheked;
        }

        internal bool InsertAlbaran(List<EskanerIrakurria> eskanerIrakurrias)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            bool emaitza = true;
            int result = 0;
            try
            {
                _objcon = _connectionConfiguration.ConnectionString;
                _objcon.Open();
                _objTrans = _objcon.BeginTransaction();


                foreach (EskanerIrakurria itemLis in eskanerIrakurrias)
                {
                    String sSql = "select count(*) AS TableCount from ofertas";

                    sSql = "insert into RECEP_SUBCON (ID_ETIQUETA, PEDIDO, O_PED,CANTIDAD, SUBCON, POS_SUBCON, PEDIDO_ORIGEN, O_PED_ORIGEN, USUARIO, N_ALBARAN_COMPRA, QR_SN, F_RECEPCION) VALUES" +
                        " (" + itemLis.IdEtiqueta + "," + itemLis.Pedido + ",'" + itemLis.Pos +
                        "'," + itemLis.Cant + "," + itemLis.Subcon + "," +
                        "'" + itemLis.PosSubcon + "'," + itemLis.PedOrigen + "," +
                        "'" + itemLis.PosOrigen + "','" + itemLis.Usuario + "','" + itemLis.Albaran +
                        "','S',(select cast('now' as timestamp)from rdb$database)"+
                         ")";




                    result = SqlFbHelper.ExecuteNonQuery(_objTrans, CommandType.Text, sSql);
                }
                _objTrans.Commit();


            }
            catch (FbException ex1)
            {
                //Log log = new Log();
                //log.WriteLog("GetAlbaran - " + ex.Message.ToString());

                //ds = null;
                _objTrans.Rollback();
                throw (ex1);
            }
            catch (Exception ex)
            {
                //Log log = new Log();
                //log.WriteLog("GetAlbaran - " + ex.Message.ToString());

                //ds = null;
                _objTrans.Rollback();
                throw (ex);
            }
            finally
            {
                if (_objcon.State == ConnectionState.Open)
                {
                    _objcon.Close();
                }
            }

            return emaitza;
        }

        internal bool Insert(List<EskanerIrakurria> eskanerIrakurrias)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            bool emaitza = true;
            int result = 0;
            try
            {
                _objcon = _connectionConfiguration.ConnectionString;
                _objcon.Open();
                _objTrans = _objcon.BeginTransaction();
                

                foreach(EskanerIrakurria itemLis in eskanerIrakurrias)
                {
                    String sSql = "select count(*) AS TableCount from ofertas";

                    sSql = "insert into RECEP_SUBCON_BULTO (ID_BULTO, PEDIDO, O_PED,CANTIDAD, SUBCON, POS_SUBCON, PEDIDO_ORIGEN, O_PED_ORIGEN, USUARIO, N_ALBARAN_COMPRA, CANTIDAD_REAL,WIFI, F_RECEPCION) VALUES" +
                        " (" + itemLis.IdBulto + "," + itemLis.Pedido + ",'" + itemLis.Pos +
                        "'," + itemLis.Cant + "," + itemLis.Subcon + "," +
                        "'" + itemLis.PosSubcon + "'," + itemLis.PedOrigen + "," +
                        "'" + itemLis.PosOrigen + "','" + itemLis.Usuario + "'," + itemLis.Albaran + "," +
                        "" + itemLis.CantReal + ",'" + itemLis.Localizacion+
                        "', (select cast('now' as timestamp)from rdb$database)" +
                         ")";




                    result = SqlFbHelper.ExecuteNonQuery(_objTrans, CommandType.Text, sSql);
                }
                _objTrans.Commit();


            }
            catch (FbException ex1)
            {
                //Log log = new Log();
                //log.WriteLog("GetAlbaran - " + ex.Message.ToString());

                //ds = null;
                _objTrans.Rollback();
                throw (ex1);
            }
            catch (Exception ex)
            {
                //Log log = new Log();
                //log.WriteLog("GetAlbaran - " + ex.Message.ToString());
                
                //ds = null;
                _objTrans.Rollback();
                throw (ex);
            }
            finally
            {
                if (_objcon.State == ConnectionState.Open)
                {
                    _objcon.Close();
                }
            }
       
                return emaitza;
        }
    }
}