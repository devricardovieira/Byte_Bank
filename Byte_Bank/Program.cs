﻿using System;
using System.Net.Mail;
using System.Net.WebSockets;

namespace Bank_Byte
{
    class Program
    {

        static void InserirNovoUsuario(List<string> senha, List<string> titulares, List<string> cpfs, List<double> saldos)
        {
            int menuNewUser;
            do
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("---------------------------------");
                Console.WriteLine("||::::Inserir Novo Usuário:::::||");
                Console.WriteLine("---------------------------------");
                Console.ResetColor();
                Console.Write("Digite o nome do titular: ");
                titulares.Add(Console.ReadLine());
                Console.Write("Digite o cpf do titular: ");
                cpfs.Add(Console.ReadLine());
                Console.Write("Digite a senha: ");
                senha.Add(Console.ReadLine());
                saldos.Add(0);

                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("(1) - Inserir Novo Usuário || (2) - Voltar ao Menu Principal");
                menuNewUser = int.Parse(Console.ReadLine());

                if (menuNewUser == 2)
                {
                    return;
                }
            } while (menuNewUser == 1);
        }
        static void DeletarUsuario(List<string> senha, List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            int menuDeletarUsuario;
            do
            {
                Console.Clear();
                Console.WriteLine("___ Deletar Usuário ___ ");
                Console.WriteLine();

                Console.Write("Digite o cpf do usuário a ser deletado: ");
                string cpfDelet = Console.ReadLine();
                int indexDelet = cpfs.FindIndex(x => x == cpfDelet);

                if (indexDelet == -1)
                {
                    Console.WriteLine("Conta não encontrada.");
                }
                else
                {
                    Console.Write("Digite sua senha:");
                    bool senhaDelet = senha.Contains(Console.ReadLine());
                    if (!senhaDelet)
                    {
                        Console.WriteLine("Senha inválida!");
                    }
                    else
                    {
                        cpfs.Remove(cpfDelet);
                        titulares.RemoveAt(indexDelet);
                        senha.RemoveAt(indexDelet);
                        Console.WriteLine("Conta deletada!!!");
                    }
                }

                Console.WriteLine("(1) Realizar nova operação || (2) Voltar ao Menu Principal");
                menuDeletarUsuario = int.Parse(Console.ReadLine());

                if (menuDeletarUsuario == 2)
                {
                    return;
                }
            } while (menuDeletarUsuario == 1);
        }
        static void VisualizarContas(List<string> cpfs, List<string> titulares, List<double> saldos, List<double> depositos, List<double> saques)
        {
            int menuListar;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("---------------------------------");
                Console.WriteLine("||::::Listagem de contas:::::||");
                Console.WriteLine("---------------------------------");
                Console.ResetColor();
                for (int i = 0; i < cpfs.Count; i++)
                {
                    Console.WriteLine("-----------------------------------------------------------------");
                    Console.WriteLine($"Nome: {titulares[i]} || CPF: {cpfs[i]} || Saldo: {saldos[i]:F2}");
                    Console.WriteLine("-----------------------------------------------------------------");

                }
                Console.WriteLine("Pressione 0 para sair");
                menuListar = int.Parse(Console.ReadLine());

            } while (menuListar != 0);
        }

        static void DepositoContas(List<string> cpfs, List<string> senha, List<double> depositos, List<double> saldos, List<double> saques)
        {
            bool menuDeposito = true;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("---------------------");
                Console.WriteLine("||::::Depósito:::::||");
                Console.WriteLine("---------------------");
                Console.ResetColor();
                Console.Write("Digite o cpf: ");
                //bool cpfDeposito = cpfs.Contains(Console.ReadLine());
                int indexCpf = cpfs.FindIndex(x => x == Console.ReadLine());
                if (indexCpf == -1)
                {
                    Console.WriteLine("CPF não encontrado!");
                }
                else
                {
                    Console.Write("Digite a senha: ");
                    //string senhaDeposito = Console.ReadLine();
                    int indexSenha = senha.FindIndex(x => x == Console.ReadLine());

                    if (indexCpf == indexSenha)
                    {
                        Console.Write("Digite o valor do Depósito:");
                        double deposito = double.Parse(Console.ReadLine());
                        depositos.Insert(indexSenha, deposito);
                        saldos.Insert(indexSenha, deposito);
                    }
                    else
                    {
                        Console.WriteLine("Senha inválida!!!");
                    }

                }
                Console.WriteLine("(1) Realizar nova operação || (2) Voltar ao Menu Principal");
                int option = int.Parse(Console.ReadLine());

                if (option == 2)
                {
                    menuDeposito = false;
                    return;
                }
            } while (menuDeposito);

        }
        static void SaqueContas(List<string> cpfs, List<string> senha, List<double> depositos, List<double> saques, List<double> saldos)
        {
            bool menuSaque = true;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("---------------------");
                Console.WriteLine("||::::Saque:::::||");
                Console.WriteLine("---------------------");
                Console.ResetColor();
                Console.Write("Digite o cpf: ");

                int indexCpf = cpfs.FindIndex(x => x == Console.ReadLine());
                if (indexCpf == -1)
                {
                    Console.WriteLine("CPF não encontrado!");
                }
                else
                {
                    Console.Write("Digite a senha: ");
                    int indexSenha = senha.FindIndex(x => x == Console.ReadLine());

                    if (indexCpf == indexSenha)
                    {
                        Console.Write("Digite o valor do Saque:");
                        double saque = double.Parse(Console.ReadLine());
                        saques.Insert(indexSenha, saque);
                        saldos.Insert(indexSenha, depositos[indexSenha] - saque);
                    }
                    else
                    {
                        Console.WriteLine("Senha inválida!!!");
                    }

                }
                Console.WriteLine("(1) Realizar nova operação || (2) Voltar ao Menu Principal");
                int option = int.Parse(Console.ReadLine());

                if (option == 2)
                {
                    menuSaque = false;
                    return;
                }
            } while (menuSaque);
        }
        static void MovimentarContas(List<string> cpfs, List<string> senha, List<double> saldos, List<double> depositos, List<double> saques)
        {
            int menuMovimentar;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("---------------------------------");
                Console.WriteLine("||::::Movimentar Contas:::::||");
                Console.WriteLine("---------------------------------");
                Console.ResetColor();

                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("╔═══════════════ Menu ══════════════╗");
                Console.WriteLine("║ 1) - Saque                        ║");
                Console.WriteLine("║ 2) - Transferência                ║");
                Console.WriteLine("║ 3) - Depósito                     ║");
                Console.WriteLine("║ 4) - Saldo                        ║");
                Console.WriteLine("║ 0) - Sair                         ║");
                Console.WriteLine("╚═══════════════════════════════════╝");
                Console.Write("Escolha a opção desejada: ");
                menuMovimentar = int.Parse(Console.ReadLine());
                switch (menuMovimentar)
                {
                    case 0:
                        return;
                    case 1:
                        SaqueContas(cpfs, senha, depositos, saques, saldos);
                        break;
                    case 3:
                        DepositoContas(cpfs, senha, depositos, saldos, saques);
                        break;
                }

            } while (menuMovimentar != 0);

        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("▒▒▒▒▒▒▒▒▒▒▒▒ Byte Bank ▒▒▒▒▒▒▒▒▒▒▒▒▒▒");
            Console.WriteLine("-------------------------------------");
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("╔═══════════════ Menu ══════════════╗");
            Console.WriteLine("║ 1) - Inserir novo usuário         ║");
            Console.WriteLine("║ 2) - Deletar usuário              ║");
            Console.WriteLine("║ 3) - Visualizar contas registradas║");
            Console.WriteLine("║ 4) - Movimentar conta             ║");
            Console.WriteLine("║ 5) - Manipular conta              ║");
            Console.WriteLine("║ 6) - Adicionar administradores    ║");
            Console.WriteLine("║ 0) - Sair                         ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.Write("Escolha a opção desejada: ");

        }
        static void Main(string[] args)
        {
            int option;

            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<double> saldos = new List<double>();
            List<string> senha = new List<string>();
            List<double> depositos = new List<double>();
            List<double> saques = new List<double>();
            do
            {
                ShowMenu();
                option = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (option)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Finalizando o Programa...");
                        Console.ResetColor();
                        break;
                    case 1:
                        InserirNovoUsuario(senha, titulares, cpfs, saldos);
                        break;
                    case 2:
                        DeletarUsuario(senha, cpfs, titulares, saldos);
                        break;
                    case 3:
                        VisualizarContas(cpfs, titulares, saldos, depositos, saques);
                        break;
                    case 4:
                        MovimentarContas(cpfs, senha, saldos, depositos, saques);
                        break;
                    case 6:

                        break;
                    case 7:
                        Console.WriteLine("Oia");
                        break;
                }

            } while (option != 0);


        }

    }
}