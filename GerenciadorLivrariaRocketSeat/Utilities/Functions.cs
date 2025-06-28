using System.Text;
using System.Text.RegularExpressions;

namespace GerenciadorLivrariaRocketSeat.Utilities
{
    /// <summary>
    /// Classe utilitária contendo funções auxiliares usadas no sistema de gerenciamento de livros.
    /// </summary>
    public class Functions
    {
        /// <summary>
        /// Escreve uma mensagem de log em um arquivo de log específico para o método.
        /// </summary>
        /// <param name="metodo">O nome do método que gerou o log.</param>
        /// <param name="msg">A mensagem a ser escrita no log.</param>
        public static void EscreveLog(string metodo, string msg)
        {
            string logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs", metodo);
            string logPath = Path.Combine(logDirectory, $"Log_{DateTime.Now:ddMMyyyyhhmmssfff}.txt");

            try
            {
                // Verifica se o diretório existe e cria se não existir
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                // Escreve no arquivo de log
                using (StreamWriter sw = new StreamWriter(logPath, false))
                {
                    sw.Write(msg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao escrever no arquivo de log: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove todos os caracteres não numéricos de uma string e retorna apenas os dígitos.
        /// </summary>
        /// <param name="Texto">A string contendo caracteres numéricos e não numéricos.</param>
        /// <returns>A string contendo apenas os dígitos encontrados na string original.</returns>
        public static string SomenteNumeros(string Texto)
        {
            //Usando Expressões Regulares
            Regex re = new Regex("[0-9]");
            StringBuilder s = new StringBuilder();
            foreach (Match m in re.Matches(Texto))
            {
                s.Append(m.Value);
            }

            return s.ToString();
        }

        /// <summary>
        /// Verifica se uma string representa um valor numérico.
        /// </summary>
        /// <param name="Valor">A string a ser verificada.</param>
        /// <param name="PermiteNegativo">Indica se valores negativos são permitidos (padrão: true).</param>
        /// <returns>Verdadeiro se a string for um valor numérico válido, caso contrário, falso.</returns>
        public static bool IsNumeric(string Valor, bool PermiteNegativo = true)
        {
            decimal resultado;
            if (decimal.TryParse(Valor, out resultado))
            {
                if (resultado < 0 && !PermiteNegativo)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
