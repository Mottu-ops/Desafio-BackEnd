using Microsoft.EntityFrameworkCore;
using rent_core_api.Model;
using rent_core_api.Repository;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace rent_core_api.Service
{
    public class LocacaoServiceImpl : LocacaoService
    {
        private readonly EntregadorRepository _entregadorRepository;
        private readonly PlanoRepository _planoRepository;
        private readonly LocacaoRepository _locacaoRepository;

        public LocacaoServiceImpl(EntregadorRepository entregadorRepository, PlanoRepository planoRepository, LocacaoRepository locacaoRepository)
        {
            this._entregadorRepository = entregadorRepository ?? throw new ArgumentNullException(nameof(entregadorRepository));
            this._planoRepository = planoRepository ?? throw new ArgumentNullException( nameof(planoRepository));
            this._locacaoRepository = locacaoRepository ?? throw new ArgumentNullException(nameof(_locacaoRepository));
        }
        public Locacao CriarLocacao(int idMoto, int idEntregador, int idPlano)
        {
            var entregador = _entregadorRepository.GetById(idEntregador);
            var plano = _planoRepository.GetById(idPlano);

            if (entregador == null || plano == null)
                throw new Exception("Entregador ou Plano não encontrado.");

            if (entregador.tipoCnh != "A")
                throw new Exception("Somente entregadores habilitados na categoria A podem efetuar uma locação.");

            var dataInicial = DateTime.Now.AddDays(1);
            var dataPrevisao = dataInicial.AddDays(plano.Dias);

            var dataInicialStr = dataInicial.ToString("yyyy-MM-dd");
            var dataPrevisaoStr = dataPrevisao.ToString("yyyy-MM-dd");

            var locacao = new Locacao
            {
                IdMoto = idMoto,
                IdEntregador = idEntregador,
                IdPlano = idPlano,
                DataInicial = dataInicialStr,
                DataPrevisao = dataPrevisaoStr,
                DataFim = dataPrevisaoStr,
                ValorTotal = plano.ValorDiario * plano.Dias
            };

            _locacaoRepository.Add(locacao);
            return locacao;
        }

        public Locacao FinalizarLocacao(int idLocacao, DateTime dataDevolucao)
        {
            var locacao = _locacaoRepository.GetById(idLocacao);
            var plano = _planoRepository.GetById(locacao.IdPlano);


            if (locacao == null)
                throw new Exception("Locação não encontrada.");

            locacao.DataDevolucao = dataDevolucao.ToString("yyyy-MM-dd");

            DateTime dataInicial = DateTime.ParseExact(locacao.DataInicial, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime dataPrevisao = DateTime.ParseExact(locacao.DataPrevisao, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var diasDevolucao = (dataDevolucao - dataInicial).Days;

            if (dataDevolucao < dataPrevisao)
            {
                var diasNaoEfetivados = (dataPrevisao - dataDevolucao).Days;
                decimal taxaMulta = plano.Dias switch
                {
                    7 => 0.2m,
                    15 => 0.4m,
                    _ => 0
                };
                locacao.Multa = taxaMulta * (diasNaoEfetivados * plano.ValorDiario);
                locacao.ValorTotal = diasDevolucao * plano.ValorDiario + locacao.Multa.GetValueOrDefault();
            }
            else if (dataDevolucao > dataPrevisao)
            {
                var diasAdicionais = (dataDevolucao - dataPrevisao).Days;
                locacao.ValorTotal += diasAdicionais * 50m; 
            }

            _locacaoRepository.Update(locacao);
            return locacao;
        }
    }
}
