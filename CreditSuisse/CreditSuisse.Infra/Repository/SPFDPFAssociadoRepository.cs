using Carga.Generica.Core.Enum;
using Carga.Generica.Core.Interface.Factory;
using Carga.Generica.Core.Interface.Repository;
using Carga.Generica.Core.Model;
using Carga.Generica.Infra.Query;
using Microsoft.Extensions.Configuration;

namespace Carga.Generica.Infra.Repository
{
    public class SPFDPFAssociadoRepository : ISPFDPFAssociadoRepository
    {

        private readonly IConfiguration config;
        private readonly IDataFactory dataFactory;
        private readonly SPFDPFAssociadoQuery query;

        public SPFDPFAssociadoRepository(IConfiguration config, IDataFactory dataFactory, SPFDPFAssociadoQuery query)
        {
            this.config = config;
            this.dataFactory = dataFactory;
            this.query = query;
        }

        public async Task<bool> PostAssociadoAsync(SPFDPFAssociado associado)
        {
            try
            {
                //lote
                var AnoLote = await dataFactory.GetFirst<int>("SELECT MAX(LOT_INT_NR_ANO) FROM SPF_LOTES", ProjetosEnum.CONNECTION.SPFDPF);
                var SeqLote = await dataFactory.GetFirst<int>("SELECT MAX(LOT_INT_NR_SEQLOTE + 1) FROM SPF_LOTES", ProjetosEnum.CONNECTION.SPFDPF);

                var lote = new SPFDPFLoteModel();
                lote.AnoLote = AnoLote;
                lote.SeqLote = SeqLote;

                var result = await dataFactory.ExecuteCommand(query.InsertLote, lote, ProjetosEnum.CONNECTION.SPFDPF);

                //caixa
                var NrCaixa = await dataFactory.GetFirst<int>(String.Format("SELECT nvl(MAX(CAX_LNG_NR_CAIXA),1) FROM SPF_CAIXAS WHERE LOT_INT_NR_ANO = {0} AND LOT_INT_NR_SEQLOTE = {1}",AnoLote, SeqLote), ProjetosEnum.CONNECTION.SPFDPF);
                
                var caixa = new SPFDPFCaixaModel();
                caixa.NrCaixa = NrCaixa;
                caixa.AnoLote = AnoLote;
                caixa.SeqLote = SeqLote;

                result = await dataFactory.ExecuteCommand(query.InsertCaixa, caixa, ProjetosEnum.CONNECTION.SPFDPF);


                //associado
                var Id = await dataFactory.GetFirst<int>("SELECT SEQ_RNE.NEXTVAL FROM DUAL", ProjetosEnum.CONNECTION.SPFDPF);

                var filtro = new SPFDPFAssociadoModel(associado, AnoLote, SeqLote, NrCaixa);

                filtro.id = Id;             

                result = await dataFactory.ExecuteCommand(query.InsertAssociado, filtro, ProjetosEnum.CONNECTION.SPFDPF);

                //imagens
                var imagem = new SPFDPFImagemModel();
                imagem.tipoImagem = "A";
                imagem.imagem = associado.imgAssinatura;
                imagem.tamanho = associado.imgAssinatura.Length;
                imagem.id = Id;

                result = await dataFactory.ExecuteCommand(query.InsertImagem, imagem, ProjetosEnum.CONNECTION.SPFDPF);

                imagem = new SPFDPFImagemModel();
                imagem.tipoImagem = "F";
                imagem.imagem = associado.imgFoto;
                imagem.tamanho = associado.imgFoto.Length;
                imagem.id = Id;

                result = await dataFactory.ExecuteCommand(query.InsertImagem, imagem, ProjetosEnum.CONNECTION.SPFDPF);

                imagem = new SPFDPFImagemModel();
                imagem.tipoImagem = "D";
                imagem.imagem = associado.imgDigital;
                imagem.tamanho = associado.imgDigital.Length;
                imagem.id = Id;

                result = await dataFactory.ExecuteCommand(query.InsertImagem, imagem, ProjetosEnum.CONNECTION.SPFDPF);

                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<IEnumerable<SPFDPFAssociado>> GetAssociadoAsync(SPFDPFAssociadoConsulta associado)
        {
            var listaAssociado = await dataFactory.Query<SPFDPFAssociado>(query.GetAssociado, associado, ProjetosEnum.CONNECTION.SPFDPF);

            foreach (var item in listaAssociado)
            {
                var imagens = await dataFactory.Query<SPFDPFImagemModel>(query.GetImagens, item, ProjetosEnum.CONNECTION.SPFDPF);

                var foto = imagens.Where(x => x.tipoImagem.Equals("F")).FirstOrDefault();
                var digital = imagens.Where(x => x.tipoImagem.Equals("D")).FirstOrDefault();
                var assinatura = imagens.Where(x => x.tipoImagem.Equals("A")).FirstOrDefault();

                item.imgFoto = foto.imagem;
                item.imgDigital = digital.imagem;
                item.imgAssinatura = assinatura.imagem;
            }         

            return listaAssociado;
        }

    }
}
