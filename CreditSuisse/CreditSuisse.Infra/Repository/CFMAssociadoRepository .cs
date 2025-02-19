using Carga.Generica.Core.Enum;
using Carga.Generica.Core.Interface.Factory;
using Carga.Generica.Core.Interface.Repository;
using Carga.Generica.Core.Model;
using Carga.Generica.Infra.Query;
using Microsoft.Extensions.Configuration;

namespace Carga.Generica.Infra.Repository
{
    public class CFMAssociadoRepository : ICFMAssociadoRepository
    {

        private readonly IConfiguration config;
        private readonly IDataFactory dataFactory;
        private readonly CFMAssociadoQuery query;

        public CFMAssociadoRepository(IConfiguration config, IDataFactory dataFactory, CFMAssociadoQuery query)
        {
            this.config = config;
            this.dataFactory = dataFactory;
            this.query = query;
        }

        public async Task<bool> PostAssociadoAsync(CFMAssociado associado)
        {
            try
            {
                //Associado
                var Id = await dataFactory.GetFirst<int>("SELECT SEQIDASSOCIADO.NEXTVAL FROM DUAL", ProjetosEnum.CONNECTION.CFM);

                var filtro = new CFMAssociadoModel(associado);

                filtro.Id = Id;             

                var result = await dataFactory.ExecuteCommand(query.InsertAssociado, filtro, ProjetosEnum.CONNECTION.CFM);

                //Digital
                var IdDigital = await dataFactory.GetFirst<int>("SELECT SEQIDDIGITAL.NEXTVAL FROM DUAL", ProjetosEnum.CONNECTION.CFM);
                filtro.IdDigital = IdDigital;
                filtro.digital_tmp = associado.digital_portador;

                result = await dataFactory.ExecuteCommand(query.InsertDigital, filtro, ProjetosEnum.CONNECTION.CFM);

                //Assinatura
                result = await dataFactory.ExecuteCommand(query.InsertAssinatura, filtro, ProjetosEnum.CONNECTION.CFM);

                //LogImportAssociado
                var IdLog = await dataFactory.GetFirst<int>("SELECT SEQIDLOGIMPORTASSOCIADO.NEXTVAL FROM DUAL", ProjetosEnum.CONNECTION.CFM);

                filtro.IdLog = IdLog;

                result = await dataFactory.ExecuteCommand(query.InsertLogImportAssociado, filtro, ProjetosEnum.CONNECTION.CFM);

                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<IEnumerable<CFMAssociado>> GetAssociadoAsync(CFMAssociadoConsulta associado)
        {
            var listaAssociado = await dataFactory.Query<CFMAssociado>(query.Get, associado, ProjetosEnum.CONNECTION.CFM);
            return listaAssociado;
        }

    }
}
