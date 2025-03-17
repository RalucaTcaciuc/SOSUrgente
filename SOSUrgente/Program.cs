using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using LibrarieModele;
using NivelStocareDate;
using SituatiiUrgenta;

namespace SOSUrgente
{
    class Program
    {
        static void Main()
        {
            List<Angajat> angajati = Administrare_angajati_FisierText.CitesteAngajatiDinFisier();
            List<Urgente> urgente = Administrare_urgente_FisierText.CitesteUrgenteDinFisier();
            Administrare_angajati_FisierText adminFisierAngajati = new Administrare_angajati_FisierText("angajati.txt");
            Administrare_urgente_FisierText adminFisierUrgente = new Administrare_urgente_FisierText("urgente.txt");
            Administrare_angajati_Memorie adminMemorieAngajati = new Administrare_angajati_Memorie();
            Administrare_urgente_Memorie adminMemorieUrgente = new Administrare_urgente_Memorie();
            while (true)
            {
                Console.WriteLine("\nMeniu:");
                Console.WriteLine("1. Adauga Angajat");
                Console.WriteLine("2. Afiseaza Angajati");
                Console.WriteLine("3. Cautare Angajat");
                Console.WriteLine("4. Scrie Angajati in Fisier");
                Console.WriteLine("5. Adauga Urgenta");
                Console.WriteLine("6. Afiseaza Urgente");
                Console.WriteLine("7. Cautare Urgenta");
                Console.WriteLine("8. Scrie Urgente in Fisier");
                Console.WriteLine("0. Iesi");
                Console.Write("Alegeti optiunea: ");
                string optiune = Console.ReadLine() ?? string.Empty;

                switch (optiune)
                {
                    case "1":
                        Console.Write("Introduceti numele angajatului: ");
                        string nume = Console.ReadLine();

                        Console.Write("Introduceti profesia: ");
                        string profesie = Console.ReadLine();
                        Console.Write("Introduceti data nasterii a angajatului: ");
                        DateTime dataNasterii;
                        while (true)
                        {
                            Console.Write("Introdu data nasterii (format: dd/MM/yyyy): ");
                            string inputData = Console.ReadLine() ?? string.Empty;
                            if (DateTime.TryParseExact(inputData, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNasterii))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Data este invalida");
                            }
                        }
                        Console.Write("Introduceti email: ");
                        string email = Console.ReadLine();
                        Console.Write("Introduceti vechime: ");
                        int vechime = Convert.ToInt32(Console.ReadLine());
                        Angajat angajat = new Angajat(nume,dataNasterii, profesie, vechime, email);
                        //Console.Write("Introduceti salariul: ");
                        //if (double.TryParse(Console.ReadLine(), out double salariu))
                        //{
                        //    Angajat angajat = new Angajat(nume, profesie);
                        //    bool adaugat = admin.AdaugaAngajat(angajati);
                        //    Console.WriteLine(adaugat ? "Angajat adaugat cu succes!" : "Eroare: Lista este plina!");
                        //}
                        //else
                        //{
                        //    Console.WriteLine("Salariul trebuie sa fie un numar valid.");
                        //}
                        bool adaugat = adminMemorieAngajati.AddAngajat(angajat);
                        Console.WriteLine(adaugat ? "Angajat adaugat cu succes!" : "Eroare: Lista este plina!");
                        break;

                    case "2":
                        foreach (var Angajat in angajati)
                        {
                            Console.WriteLine(Angajat.Info());
                        }
                        break;

                    case "3":
                        Console.Write("Introdu numele angajatului cautat: ");
                        string numeCautat = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;
                        var rezultateNume = Administrare_angajati_Memorie.CautaAngajatDupaNume(angajati, numeCautat);

                        if (rezultateNume.Count == 0)
                            Console.WriteLine("Nicio persoana gasita.");
                        else
                            rezultateNume.ForEach(a => Console.WriteLine(a.Info()));
                        break;

                    case "4":
                        Administrare_angajati_FisierText.ScrieAngajatiInFisier(angajati);
                        Console.WriteLine("Angajatii au fost salvati in fisier.");
                        break;

                    case "5":
                        Console.Write("Introduceti orasul: ");
                        string oras = Console.ReadLine();

                        Console.Write("Introduceti strada: ");
                        string strada = Console.ReadLine();

                        Console.Write("Introduceti numarul: ");
                        if (int.TryParse(Console.ReadLine(), out int numar))
                        {
                            Urgente urgenta = new Urgente(oras, strada, numar);
                            urgente.Add(urgenta);
                            Console.WriteLine("Urgenta a fost adaugata.");
                        }
                        else
                        {
                            Console.WriteLine("Numarul trebuie sa fie un numar valid.");
                        }
                        break;

                    case "6":
                        urgente.ForEach(u => Console.WriteLine(u.Info()));
                        break;

                    case "7":
                        Console.Write("Introduceti orasul pentru cautare urgenta: ");
                        string orasCautat = Console.ReadLine();
                        var urgentaGasita = urgente.FirstOrDefault(u => u.oras.ToLower() == orasCautat.ToLower());

                        if (urgentaGasita != null)
                            Console.WriteLine(urgentaGasita.Info());
                        else
                            Console.WriteLine("Nicio urgenta gasita in acest oras.");
                        break;

                    case "8":
                        Administrare_urgente_FisierText.ScrieUrgenteInFisier(urgente);
                        Console.WriteLine("Urgentele au fost salvate in fisier.");
                        break;

                    case "0":
                        Console.WriteLine("Iesire din aplicatie...");
                        return;

                    default:
                        Console.WriteLine("Optiune invalida.");
                        break;
                }
            }
        }
    }
}
