using System;
using System.Threading;

namespace ConsoleBanking
{
	internal class Menu
	{
	     // Transaction page
		public static void MainMenu()
		{
			Designs.CenterNewLine("\n\n\n\n");
			Console.BackgroundColor = ConsoleColor.DarkMagenta;
			Designs.DrawLine();

			Console.WriteLine($"{Designs.AlignText(0, "")}");
			Console.WriteLine($"{Designs.AlignText(37, "1. Withdraw ")}");
			Console.WriteLine($"{Designs.AlignText(37, "2. Deposit M")}");
			Console.WriteLine($"{Designs.AlignText(37, "3. Account Balance")}");
			Console.WriteLine($"{Designs.AlignText(37, "4. Account Details")}");
			Console.WriteLine($"{Designs.AlignText(37, "5. Transaction History")}");
			Console.WriteLine($"{Designs.AlignText(37, "6. Logout")}");

			Designs.DrawLine();
			Console.BackgroundColor = ConsoleColor.Black;

			ConsoleKeyInfo userOption = Console.ReadKey();
			Console.Clear();

			switch (userOption.Key)
			{
				case ConsoleKey.NumPad1:
				Transactions.MakeWithdrawal();
					break;

				case ConsoleKey.NumPad2:
					Transactions.MakeDeposit();
					break;

				case ConsoleKey.NumPad3:
					Transactions.CheckAccountBalance();
					break;

				case ConsoleKey.NumPad4:
					Transactions.ViewAccountDetails();
					break;

				
				case ConsoleKey.NumPad5:
					Transactions.ViewAllTransactions();
                    Console.WriteLine();
					ReturnToMenu();
					break;

				case ConsoleKey.NumPad6:
					Console.Clear();
					HomeMenu();
					break;

				default:
					Designs.CenterNewLine("Wrong Input! \n");
					Thread.Sleep(1500);

					Console.Clear();
					MainMenu();
					break;
			}
		}
	
		// Home page
		public static void HomeMenu()
		{
			Designs.CenterNewLine("Welcome...\n\n\n\n");
			Console.BackgroundColor = ConsoleColor.DarkMagenta;	
			Designs.DrawLine();
				
			Console.WriteLine($"{Designs.AlignText(0, "")}");
			Console.WriteLine($"{Designs.AlignText(35, "1. Login for Existing Customers")}");
			Console.WriteLine($"{Designs.AlignText(35, "2. Open a new Account")}");
			Console.WriteLine($"{Designs.AlignText(35, "3. About Us")}");
			Console.WriteLine($"{Designs.AlignText(35, "4. Exit")}");
			Console.WriteLine($"{Designs.AlignText(0, "")}");

			Designs.DrawLine();
			Console.BackgroundColor = ConsoleColor.Black;

			ConsoleKeyInfo userOption = Console.ReadKey();
			Notifications.WaitWindow();
			
			Console.Clear();

			switch (userOption.Key)
			{
				case ConsoleKey.NumPad1:
					Console.Clear();
					Login.LogInMenu();			
					break;

				case ConsoleKey.NumPad2:
					Console.Clear();
					Transactions.OpenNewAccount();
					break;

				case ConsoleKey.NumPad3:
					Console.Clear();
					break;

				case ConsoleKey.NumPad4:
					Console.Clear();
					Designs.CenterNewLine("Thank you for Banking with us.");

					Environment.Exit(0);
					break;

				default:
					Designs.CenterNewLine("Wrong Input!");
					Thread.Sleep(1500);

					Console.Clear();
					HomeMenu();
					break;
			}
		}


		// Back to Transaction page
		public static void ReturnToMenu()
		{
			Console.WriteLine("0. Main Menu.");
			
			ConsoleKeyInfo userOption = Console.ReadKey();

			if (userOption.Key == ConsoleKey.NumPad0)
			{
				Console.Clear();
				MainMenu();
			}
			else
			{
				Console.Clear();
				Designs.CenterNewLine("Wrong Input!");

				HomeMenu();
			}
		}
	}
}
