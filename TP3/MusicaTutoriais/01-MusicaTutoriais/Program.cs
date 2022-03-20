using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_MusicaTutoriais
{
    internal class Program
    {
        static void Main(string[] args)
        {

            MenuHandler(MostrarMenu());

            int MostrarMenu()
            {
                Console.WriteLine("Escolha uma das opções abaixo");
                Console.WriteLine("------------------------------");
                Console.WriteLine("1 - Pesquisar tutoriais");
                Console.WriteLine("2 - Adicionar novo tutorial");
                Console.WriteLine("0 - Sair");

                string opcao_string = Console.ReadLine();
                int opcao = Int32.Parse(opcao_string);

                return opcao;

            }

            void MenuHandler(int escolha)
            {
                if (escolha == 1)
                {
                    PesquisarTutorial();
                }
                else if (escolha == 2)
                {
                    AdicionaTutorial();
                    MenuHandler(MostrarMenu());
                }
                else if (escolha == 0)
                {
                    System.Environment.Exit(1);
                }
            }
        }

        static void AdicionaTutorial()
        {
            var tutorial = new Tutorial();
            Console.WriteLine("Digite o título do tutorial: ");
            string titulo = Console.ReadLine();

            Console.WriteLine("Digite o instrutor do tutorial: ");
            string instrutor = Console.ReadLine();

            Console.WriteLine("Digite a data máxima para inscrições gratuitas(dd/MM/yyyy): ");
            string formato = "dd/MM/yyyy";
            DateTime data = DateTime.ParseExact(Console.ReadLine(), formato, null);

            Console.WriteLine("Digite o tamanho do curso em horas: ");
            string horas_aula = Console.ReadLine();
            int horas = Int32.Parse(horas_aula);

            Console.WriteLine("");
            Console.WriteLine("Os dados abaixo estão corretos?");
            Console.WriteLine("Título: {0}", titulo);
            Console.WriteLine("Instrutor: {0}", instrutor);
            Console.WriteLine("Data máxima para inscrições gratuitas: {0}", data.ToString("dd/MM/yyyy"));
            Console.WriteLine("Total de horas: {0}", horas);

            Console.WriteLine("1 - Sim");
            Console.WriteLine("2 - Não");

            string opcao_string = Console.ReadLine();
            int opcao = Int32.Parse(opcao_string);

            if (opcao == 1)
            {
                tutorial.SetTitulo(titulo);
                tutorial.SetIntrutor(instrutor);
                tutorial.SetPeriodoGratuito(data);
                tutorial.SetHorasAulas(horas);
                TutorialRepository.Create(tutorial);

                Console.WriteLine("Dados adicionados com sucesso!");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Título: {0}", tutorial.GetTitulo());
                Console.WriteLine("Instrutor: {0}", tutorial.GetInstrutor());
                tutorial.CalcularPeriodo(data);
                Console.WriteLine("Total de horas: {0}", tutorial.GetHorasAula());
                Console.WriteLine();
            }
            else if (opcao == 2)
            {
                AdicionaTutorial();
            }
        }

        static void PesquisarTutorial()
        {
            const string pressioneQualquerTecla = "Pressione qualquer tecla para exibir o menu principal ...";
            Console.WriteLine("Digite o título, ou parte do título, do tutorial que deseja encontrar: ");
            string titulo = Console.ReadLine();
            var achados = TutorialRepository.GetBySearch(titulo);

            if (achados.Count > 0)
            {
                Console.WriteLine("Selecione uma das opções abaixo para visualizar os dados dos tutoriais encontrados:");
                for (var index = 0; index < achados.Count; index++)
                {
                    Console.WriteLine($"{index} - {achados[index].GetTitulo()}");
                }

                if (!ushort.TryParse(Console.ReadLine(), out var indexAExibir) || indexAExibir >= achados.Count)
                {
                    Console.WriteLine($"Opção inválida! {pressioneQualquerTecla}");
                    Console.ReadKey();
                }

                if (indexAExibir < achados.Count)
                {
                    var entidade = achados[indexAExibir];

                    Console.WriteLine("Título: {0}", entidade.GetTitulo());
                    Console.WriteLine("Instrutor: {0}", entidade.GetInstrutor());
                    Console.WriteLine("Preço: {0}", entidade.GetPreco()); 
                    Console.WriteLine("Total de horas: {0}", entidade.GetHorasAula());

                    Console.Write(pressioneQualquerTecla);
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine($"Não foi encontrado nenhum tutorial! {pressioneQualquerTecla}");
                    Console.ReadKey();
                }
            }
        }
    }
}
