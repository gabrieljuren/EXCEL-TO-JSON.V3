using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Relatorio_api.Models;
using Relatorio_api.DAO;

namespace Relatorio_api.Repositorio
{
    public class valoresRepositorio
    {
        RelatorioDao relatorioDao = new RelatorioDao();
        private List<RelatorioModel> valores;

        RelatorioDao relatorio;

        public valoresRepositorio()
        {
            Inicializador();
        }

        private void Inicializador()
        {
            valores = relatorioDao.SPS_RELATORIO();
        }

        public IEnumerable<RelatorioModel> All
        {
            get
            {
                return valores;
            }
        }
    }
}