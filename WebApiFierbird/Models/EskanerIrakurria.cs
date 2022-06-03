using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiFierbird.Models
{
    public class EskanerIrakurria
    {

        private int albaran;
        private int subcon;
        private String posSubcon;

        private int codigo;
        private int pedOrigen;
        private String posOrigen;

        private int pedido;
        private String pos;
        private String refe;
        private int cant;
        private int cantReal;
        private int idBulto;
        private String usuario;
        private String localizacion;
        private int idEtiqueta;

        public EskanerIrakurria(int albaran, int subcon, string posSubcon, int codigo, int pedOrigen
            , string posOrigen, int pedido, string pos, string refe, int cant, int cantReal, int idBulto
            , string usuario, string localizacion, int idEtiqueta)
        {
            this.albaran = albaran;
            this.subcon = subcon;
            this.posSubcon = posSubcon;
            this.codigo = codigo;
            this.pedOrigen = pedOrigen;
            this.posOrigen = posOrigen;
            this.pedido = pedido;
            this.pos = pos;
            this.refe = refe;
            this.cant = cant;
            this.cantReal = cantReal;
            this.idBulto = idBulto;
            this.usuario = usuario;
            this.localizacion = localizacion;
            this.idEtiqueta = idEtiqueta;
        }

        public int Albaran { get => albaran; set => albaran = value; }
        public int Subcon { get => subcon; set => subcon = value; }
        public string PosSubcon { get => posSubcon; set => posSubcon = value; }
        public int Codigo { get => codigo; set => codigo = value; }
        public int PedOrigen { get => pedOrigen; set => pedOrigen = value; }
        public string PosOrigen { get => posOrigen; set => posOrigen = value; }
        public int Pedido { get => pedido; set => pedido = value; }
        public string Pos { get => pos; set => pos = value; }
        public string Refe { get => refe; set => refe = value; }
        public int Cant { get => cant; set => cant = value; }
        public int CantReal { get => cantReal; set => cantReal = value; }
        public int IdBulto { get => idBulto; set => idBulto = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Localizacion { get => localizacion; set => localizacion = value; }
        public int IdEtiqueta { get => idEtiqueta; set => idEtiqueta = value; }

    }
}