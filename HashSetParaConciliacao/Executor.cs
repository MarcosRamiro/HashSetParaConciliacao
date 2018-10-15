using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HashSetParaConciliacao
{
    public class Executor
    {
        private HashSet<Operacao> baseCarteira = new HashSet<Operacao>();
        private HashSet<Operacao> baseClearing = new HashSet<Operacao>();
        private Stopwatch tempo = new Stopwatch();

        public void Execute()
        {
            tempo.Start();
            CarregarBaseCarteira();
            Console.WriteLine($"Carga Carteira ==> {tempo.Elapsed}");
            tempo.Restart();
            CarregarBaseClearing();
            Console.WriteLine($"Carga Clearing ==> {tempo.Elapsed}");

            ConciliarCarteira();
            ResultadoConciliacaoCarteira();

            Console.WriteLine("Terminei");
            Console.Read();
        }

        private void ConciliarCarteira()
        {
            Console.WriteLine($"Inicio Conciliar Carteira {DateTime.Now}");

            tempo.Restart();
            //var auxCarteira = new HashSet<Operacao>(baseCarteira, new OperacaoComparer());
            var auxCarteira_SemDuplicados = new HashSet<Operacao>(baseCarteira, new OperacaoComparerRemoverDuplicado());
            var auxClearing_SemDuplicados = new HashSet<Operacao>(baseClearing, new OperacaoComparerRemoverDuplicado());

            var auxConciliacao = new HashSet<Operacao>(auxCarteira_SemDuplicados, new OperacaoComparer());

            Console.WriteLine($"Qauntidade {auxCarteira_SemDuplicados.Count}");
            auxConciliacao.SymmetricExceptWith(auxClearing_SemDuplicados);
            Console.WriteLine($"Qauntidade Depois {auxConciliacao.Count}");

            foreach (var s in auxConciliacao)
            {
                if (string.IsNullOrEmpty(s.ResultadoConciliacao))
                    s.ResultadoConciliacao = "Apenas " + Enum.GetName(typeof(OrigemOperacao), s.OrigemOperacao);
            }

            Console.WriteLine($"Tempo Conciliação Carteira ==> {tempo.Elapsed}");



        }

        private void ResultadoConciliacaoCarteira()
        {

        }

        private void CarregarBaseClearing()
        {

            // Duplicado na Clearing
            baseClearing.Add(new Operacao()
            {
                ID = 1,
                DataInicio = new DateTime(2018, 10, 13),
                DataFim = new DateTime(2019, 02, 02),
                Indexador = "IBLK",
                OrigemOperacao = OrigemOperacao.Clearing,
                Taxa = 1.22m
            });
            baseClearing.Add(new Operacao()
            {
                ID = 1,
                DataInicio = new DateTime(2018, 10, 13),
                DataFim = new DateTime(2019, 02, 02),
                Indexador = "IBL",
                OrigemOperacao = OrigemOperacao.Clearing,
                Taxa = 1.22m
            });
            baseClearing.Add(new Operacao()
            {
                ID = 1,
                DataInicio = new DateTime(2018, 10, 13),
                DataFim = new DateTime(2019, 02, 02),
                Indexador = "IBL",
                OrigemOperacao = OrigemOperacao.Clearing,
                Taxa = 1.22m
            });


            // Conciliado OK
            //baseClearing.Add(new Operacao()
            //{
            //    ID = 2,
            //    DataInicio = new DateTime(2018, 10, 13),
            //    DataFim = new DateTime(2019, 02, 02),
            //    Indexador = "CDI",
            //    OrigemOperacao = OrigemOperacao.Clearing,
            //    Taxa = 1.22m
            //});

            //// Duplicado na Carteira
            //baseClearing.Add(new Operacao()
            //{
            //    ID = 3,
            //    DataInicio = new DateTime(2018, 10, 13),
            //    DataFim = new DateTime(2019, 02, 02),
            //    Indexador = "CDI",
            //    OrigemOperacao = OrigemOperacao.Clearing,
            //    Taxa = 1.22m
            //});

            //// Taxa Divergente
            //baseClearing.Add(new Operacao()
            //{
            //    ID = 4,
            //    DataInicio = new DateTime(2018, 10, 13),
            //    DataFim = new DateTime(2019, 02, 02),
            //    Indexador = "CDI",
            //    OrigemOperacao = OrigemOperacao.Clearing,
            //    Taxa = 1.223m
            //});

            //// Indexador Divergente
            //baseClearing.Add(new Operacao()
            //{
            //    ID = 5,
            //    DataInicio = new DateTime(2018, 10, 13),
            //    DataFim = new DateTime(2019, 02, 02),
            //    Indexador = "CDO",
            //    OrigemOperacao = OrigemOperacao.Clearing,
            //    Taxa = 1.22m
            //});

            //// Somente na Clearing
            //baseClearing.Add(new Operacao()
            //{
            //    ID = 20,
            //    DataInicio = new DateTime(2018, 10, 13),
            //    DataFim = new DateTime(2019, 02, 02),
            //    Indexador = "CDI",
            //    OrigemOperacao = OrigemOperacao.Clearing,
            //    Taxa = 1.22m
            //});

            //for (long i = 100; i <= 200000; i++)
            //{
            //    baseClearing.Add(new Operacao()
            //    {
            //        ID = i,
            //        DataInicio = new DateTime(2018, 10, 13),
            //        DataFim = new DateTime(2019, 02, 02),
            //        Indexador = "CDI",
            //        OrigemOperacao = OrigemOperacao.Clearing,
            //        Taxa = 1.2m
            //    });
            //}

        }

        private void CarregarBaseCarteira()
        {



            // Esta duplicado na base Clearing
            baseCarteira.Add(new Operacao()
            {
                ID = 2,
                DataInicio = new DateTime(2018, 10, 13),
                DataFim = new DateTime(2019, 02, 02),
                Indexador = "IBL",
                OrigemOperacao = OrigemOperacao.Carteira,
                Taxa = 1.22m
            });

            //// Conciliado OK
            //baseCarteira.Add(new Operacao()
            //{
            //    ID = 2,
            //    DataInicio = new DateTime(2018, 10, 13),
            //    DataFim = new DateTime(2019, 02, 02),
            //    Indexador = "CDI",
            //    OrigemOperacao = OrigemOperacao.Carteira,
            //    Taxa = 1.22m
            //});

            //// Duplicado na Carteira
            //baseCarteira.Add(new Operacao()
            //{
            //    ID = 3,
            //    DataInicio = new DateTime(2018, 10, 13),
            //    DataFim = new DateTime(2019, 02, 02),
            //    Indexador = "CDI",
            //    OrigemOperacao = OrigemOperacao.Carteira,
            //    Taxa = 1.22m
            //});
            //baseCarteira.Add(new Operacao()
            //{
            //    ID = 3,
            //    DataInicio = new DateTime(2018, 10, 13),
            //    DataFim = new DateTime(2019, 02, 02),
            //    Indexador = "CDI",
            //    OrigemOperacao = OrigemOperacao.Carteira,
            //    Taxa = 1.22m
            //});

            //// Taxa Divergente
            //baseCarteira.Add(new Operacao()
            //{
            //    ID = 4,
            //    DataInicio = new DateTime(2018, 10, 13),
            //    DataFim = new DateTime(2019, 02, 02),
            //    Indexador = "CDI",
            //    OrigemOperacao = OrigemOperacao.Carteira,
            //    Taxa = 1.224m
            //});

            //// Indexador Divergente
            //baseCarteira.Add(new Operacao()
            //{
            //    ID = 5,
            //    DataInicio = new DateTime(2018, 10, 13),
            //    DataFim = new DateTime(2019, 02, 02),
            //    Indexador = "IBL",
            //    OrigemOperacao = OrigemOperacao.Carteira,
            //    Taxa = 1.22m
            //});

            //// Somente na Carteira
            //baseCarteira.Add(new Operacao()
            //{
            //    ID = 30,
            //    DataInicio = new DateTime(2018, 10, 13),
            //    DataFim = new DateTime(2019, 02, 02),
            //    Indexador = "CDI",
            //    OrigemOperacao = OrigemOperacao.Carteira,
            //    Taxa = 1.22m
            //});

            //for (long i = 100; i <= 200000; i++)
            //{
            //    baseCarteira.Add(new Operacao()
            //    {
            //        ID = i,
            //        DataInicio = new DateTime(2018, 10, 13),
            //        DataFim = new DateTime(2019, 02, 02),
            //        Indexador = "CDI",
            //        OrigemOperacao = OrigemOperacao.Carteira,
            //        Taxa = 1.2m
            //    });
            //}

        }

    }
}
