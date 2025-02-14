﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LoginSystem
{
    class Program
    {
        static Dictionary<string, string> userDatabase = new Dictionary<string, string>();

        static void Main(string[] args) // Overveiw of options you have in terminal, you can exit at and time by using option 3.
        {
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterUser();
                        break;
                    case "2":
                        LoginUser();
                        break;
                    case "3":
                        Console.WriteLine("Thankyou for using the Login System, GoodBye!"); 
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again!");
                        break;
                }
            }
        }

        static void RegisterUser() 
        {
            Console.Write("Enter a username: ");
            string? username = Console.ReadLine();

            if (userDatabase.ContainsKey(username))
            {
                Console.WriteLine("Username already exists. Try a different one.");
                return;
            }

            Console.Write("Enter a password: ");
            string password = GetValidInput();

            string hashedPassword = HashPassword(password);
            userDatabase[username] = hashedPassword;

            Console.WriteLine("User registered successfully!");
        }

        static void LoginUser()
        {
            Console.Write("Enter your username: ");
            string? username = Console.ReadLine();

            if (!userDatabase.ContainsKey(username))
            {
                Console.WriteLine("Username not found. Please register first.");
                return;
            }

            Console.Write("Enter your password: ");
            string password = GetValidInput();

            string storedHash = userDatabase[username];
            if (VerifyPassword(password, storedHash))
            {
                Console.WriteLine("Login successfull!");
            }
            else
            {
                Console.WriteLine("Incorrect password!");
            }
        }

        static string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }

        static string HashPassword(string password)
        {
            return ComputeSHA256Hash(password.ToLower());
        }

        static bool VerifyPassword(string inputPassword, string storeHash)
        {
            string hashinput = ComputeSHA256Hash(inputPassword.ToLower());
            return hashinput == storeHash;
        }

        static string GetValidInput()
        {
            while (true)
            {
                string? input = Console.ReadLine()?.Trim();
                if(!string.IsNullOrEmpty(input))
                {
                    return input;
                }
                Console.WriteLine("Invalid input! Please enter a valid string.");
            }
        }



    }
}