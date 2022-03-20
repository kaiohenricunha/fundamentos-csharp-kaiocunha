using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_MusicaTutoriais
{
    public class Tutorial
    {
        string titulo;
        string instrutor;
        DateTime periodo_gratuito;
        double preco;
        bool gratuito;
        int horas_aula;

        public string GetTitulo()
        {
            return titulo;
        }

        public void SetTitulo(string titulo)
        {
            this.titulo = titulo;
        }

        public string GetInstrutor()
        {
            return instrutor;
        }

        public void SetIntrutor(string instrutor)
        {
            this.instrutor = instrutor;
        }

        public DateTime GetPeriodoGratuito()
        {
            return periodo_gratuito;
        }

        public void SetPeriodoGratuito(DateTime periodo_gratuito)
        {
            this.periodo_gratuito = periodo_gratuito;
        }

        public double GetPreco()
        {
            return preco;
        }

        public void SetPreco(double preco)
        {
            this.preco = preco;
        }

        public bool GetGratuito()
        {
            return gratuito;
        }

        public void SetGratuito(bool gratuito)
        {
            this.gratuito = gratuito;
        }

        public int GetHorasAula()
        {
            return horas_aula;
        }

        public void SetHorasAulas(int horas_aula)
        {
            this.horas_aula = horas_aula;
        }

        public void CalcularPeriodo(DateTime periodo_gratuito)
        {
            string formato = "dd/MM/yyyy";

            DateTime data = DateTime.Today;
            string data_string = data.ToString("dd/MM/yyyy");
            DateTime data_atual = DateTime.ParseExact(data_string, formato, null);

            string periodo_gratuito_string = periodo_gratuito.ToString("dd/MM/yyyy");
            DateTime data_gratuita = DateTime.ParseExact(periodo_gratuito_string, formato, null);

            int compare = DateTime.Compare(data_atual, data_gratuita);

            if (compare < 0)
            {
                var diff_dias = periodo_gratuito - data_atual;
                SetGratuito(true);
                Console.WriteLine("Preço: R$ {0}", this.preco);
                Console.WriteLine("A inscrição gratuita para {0} se encerra em {1} dias.", this.titulo, diff_dias.Days);
            }

            // data_atual é depois de periodo_gratuito ou igual a periodo_gratuito
            if (compare > 0 || compare == 0)
            {
                if (compare > 0)
                {
                    double preco = 25;
                    SetPreco(preco);
                    Console.WriteLine("Preço: R$ {0}", this.preco);
                    Console.WriteLine("As inscrições gratuitas para {0} já se encerraram.", this.titulo);
                }

                else
                    {
                    Console.WriteLine("Preço: R$ {0}", this.preco);
                    Console.WriteLine("A inscrição gratuita para {0} se encerra hoje.", this.titulo);
                }
                }

            }
        }
    }
