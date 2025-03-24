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
                        Console.WriteLine("Alegeti statutul angajatului:");
                        Console.WriteLine("1. Subofiter");
                        Console.WriteLine("2. Ofiter");
                        Console.WriteLine("3. Pensionar");
                        Console.WriteLine("4. Personal Administrativ");
                        StatutAngajat statutAngajat = StatutAngajat.Subofiter; // Default
                        string alegereStatut = Console.ReadLine();
                        switch (alegereStatut)
                        {
                            case "1":
                                statutAngajat = StatutAngajat.Subofiter;
                                break;
                            case "2":
                                statutAngajat = StatutAngajat.Ofiter;
                                break;
                            case "3":
                                statutAngajat = StatutAngajat.Pensionar;
                                break;
                            case "4":
                                statutAngajat = StatutAngajat.PersonalAdministrativ;
                                break;
                            default:
                                Console.WriteLine("Statut invalid. Folosim statutul default (Subofiter).");
                                break;
                        }

                        Angajat angajat = new Angajat(nume, dataNasterii, profesie, vechime, email, statutAngajat);
                        bool adaugat = adminMemorieAngajati.AddAngajat(angajat);
                        angajati.Add(angajat);
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
                        if (Administrare_angajati_FisierText.ScrieAngajatiInFisier(angajati))
                        {
                            Console.WriteLine("Angajatii au fost salvati in fisier cu succes.");
                        }
                        else
                        {
                            //Console.WriteLine("Eroare la salvarea angajatilor în fisier.");
                        }
                        break;
                    case "5":
                        Console.Write("Introduceti orasul: ");
                        string oras = Console.ReadLine();

                        Console.Write("Introduceti strada: ");
                        string strada = Console.ReadLine();
                        int numar;
                        while (true)
                        {
                            Console.Write("Introduceti numarul: ");
                            string inputNumar = Console.ReadLine();
                            if (int.TryParse(inputNumar, out numar))
                            {
                                break;
                            }
                            Console.WriteLine("Numarul trebuie sa fie un numar valid.");
                        }
                        TipUrgente tipUrgente = TipUrgente.Accident;

                        Console.WriteLine("Alegeti tipul urgentei:");
                        Console.WriteLine("1. Accident");
                        Console.WriteLine("2. Incendiu");
                        Console.WriteLine("3. Inundatie");
                        Console.WriteLine("4. Criminalitate");

                        string alegereTipurgente = Console.ReadLine();

                       
                        switch (alegereTipurgente)
                        {
                            case "1":
                                tipUrgente = TipUrgente.Accident;
                                break;
                            case "2":
                                tipUrgente = TipUrgente.Incendiu;
                                break;
                            case "3":
                                tipUrgente = TipUrgente.Inundatie;
                                break;
                            case "4":
                                tipUrgente = TipUrgente.Criminalitate;
                                break;
                            default:
                                Console.WriteLine("Opțiune invalidă. Se va folosi valoarea implicită: Accident.");
                                break;
                        }

                    
                        Console.WriteLine($"Urgenta de tip: {tipUrgente}");

                        Urgente urgenta = new Urgente(oras, strada, numar, tipUrgente);
                        urgente.Add(urgenta);
                        Console.WriteLine("Urgenta a fost adaugata.");
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