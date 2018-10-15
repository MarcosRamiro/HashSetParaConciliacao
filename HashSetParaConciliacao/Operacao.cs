using System;
using System.Collections.Generic;

namespace HashSetParaConciliacao
{
    public enum OrigemOperacao
    {
        Carteira,
        Clearing
    }

    public class Operacao
    {
        //
        private bool TaxaConciliado = false;
        private bool IndexadorConciliado = false;
        private string _DetalheDivergencia;
        private const string SEM_DIVERGENCIA = "Sem Divergência";

        // Properties
        public long ID { get; set; }

        public OrigemOperacao OrigemOperacao { get; set; }

        public string ResultadoConciliacao { get; set; }

        public string DetalheDivergência
        {
            get
            {
                return _DetalheDivergencia;
            }
            private set
            {    
                if (string.IsNullOrEmpty(_DetalheDivergencia) || _DetalheDivergencia.Equals(SEM_DIVERGENCIA + " |"))
                    _DetalheDivergencia = value + " |";
                else if (!_DetalheDivergencia.Contains(value))
                {
                    _DetalheDivergencia = _DetalheDivergencia + " " + value + " |";
                }
            }
        }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public string Indexador { get; set; }
        
        public decimal Taxa { get; set; }
        
        // Métodos
        private void AtualizarStatusConciliacao()
        {
            if (IndexadorConciliado & TaxaConciliado)
            {
                ResultadoConciliacao = "Conciliado";
                if (string.IsNullOrEmpty(DetalheDivergência))
                    DetalheDivergência = SEM_DIVERGENCIA;
            }
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public bool CompararIndexador(Operacao outra)
        {

            bool retorno = true;
            string mensagemDivergência = "Indexador";
            if (!this.Indexador.Equals(outra.Indexador))
            {
                DetalheDivergência = mensagemDivergência;
                outra.DetalheDivergência = mensagemDivergência;
                retorno = false;
            }

            IndexadorConciliado = true;
            outra.IndexadorConciliado = true;
            AtualizarStatusConciliacao();
            outra.AtualizarStatusConciliacao();
            return retorno;
        }

        public bool CompararTaxa(Operacao outra)
        {
            bool retorno = true;
            
            string mensagemDivergência = "Taxa";
            if (!this.Taxa.Equals(outra.Taxa))
            {
                DetalheDivergência = mensagemDivergência;
                outra.DetalheDivergência = mensagemDivergência;
                retorno = false;
            }
            TaxaConciliado = true;
            outra.TaxaConciliado = true;
            AtualizarStatusConciliacao();
            outra.AtualizarStatusConciliacao();
            return retorno;
        }

    }

    public class OperacaoComparer : IEqualityComparer<Operacao>
    {
        public bool Equals(Operacao x, Operacao y)
        {
            return x.CompararTaxa(y) & x.CompararIndexador(y);
        }

        public int GetHashCode(Operacao obj)
        {
            return obj.GetHashCode();
        }

    }

    public class OperacaoComparerRemoverDuplicado : IEqualityComparer<Operacao>
    {
        public bool Equals(Operacao x, Operacao y)
        {
            y.ResultadoConciliacao = "Duplicada";
            return true;
        }

        public int GetHashCode(Operacao obj)
        {
            return obj.GetHashCode();
        }

    }

}
