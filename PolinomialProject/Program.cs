using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PolinomialProject
{
    class Program
    {
        static void Main(string[] args)
        {
            InputOutput io = new InputOutput();
            int firstMaxExp = io.GetExp();
            int secondMaxExp = io.GetExp();

            //використовую аналог vector

            List<Member> membersLst = new List<Member>();
            Handler handlerLst = new Handler(membersLst);
            handlerLst.GenerateWithExp(firstMaxExp);
            io.PrintPolinom(membersLst);

            List<Member> membersLst2 = new List<Member>();
            Handler handlerLst2 = new Handler(membersLst2);
            handlerLst2.GenerateWithExp(secondMaxExp);
            io.PrintPolinom(membersLst2);


            Console.WriteLine("Вектор: ");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<Member> result = handlerLst.Multiply(handlerLst2);
            stopwatch.Stop();

            io.PrintPolinom(result);
            Console.WriteLine("Потрачений час: " + stopwatch.Elapsed.TotalMilliseconds);

            //===========================================================================================================

            // виконую аналогічні дії з двосв'язним списком

            LinkedList<Member> membersLstLinked = new LinkedList<Member>();
            Handler handlerLstLinked = new Handler(membersLstLinked);
            handlerLstLinked.GenerateWithExp(firstMaxExp);
            io.PrintPolinom(membersLstLinked);

            LinkedList<Member> membersLst2Linked = new LinkedList<Member>();
            Handler handlerLst2Linked = new Handler(membersLst2Linked);
            handlerLst2Linked.GenerateWithExp(secondMaxExp);
            io.PrintPolinom(membersLst2Linked);


            Console.WriteLine("Зв'язний список:");

            stopwatch.Start();
            List<Member> resultLinked = handlerLst.Multiply(handlerLst2);
            stopwatch.Stop();

            io.PrintPolinom(resultLinked);
            Console.WriteLine("Потрачений час: " + stopwatch.Elapsed.TotalMilliseconds);


        }
    }

    class InputOutput
    {
        public int GetExp()
        {
            Console.WriteLine("Введiть експонент:");
            return Int32.Parse(Console.ReadLine());
        }

        public void PrintPolinom(ICollection<Member> members)
        {
            foreach (Member member in members.Reverse())
            {
                Console.Write(member.Coeff + "x^" + member.Expo+"+");
            }

            Console.WriteLine("\n-----------------------------");
        }
    }

    class Handler
    {
        public ICollection<Member> members { get; }

        public Handler(ICollection<Member> members)
        {
            this.members = members;
        }

        public void GenerateWithExp(int maxExp)
        {
            Random rndGenerator = new Random();
            for (int i = 0; i <= maxExp; i++)
            {
                members.Add(new Member(rndGenerator.Next(1, 10), i));
            }
        }

        public List<Member> Multiply(Handler polinominalController)
        {
            List<Member> result = new List<Member>();
            Handler resultController = new Handler(result);

            foreach (Member member1 in members)
            {
                foreach (Member member2 in polinominalController.members)
                {
                    resultController.addMember(member1.Multiply(member2));
                }
            }

            return result;
        }

        public void addMember(Member member)
        {
            bool founded = false;
            int exp = member.Expo;
            foreach (Member member1 in members)
            {
                if (member1.Expo == exp)
                {
                    member1.Coeff += member.Coeff;
                    founded = true;
                    break;
                }
            }
            if (!founded)
            {
                members.Add(member);
            }
        }

    }

    class Member
    {
        public double Coeff { get; set; }
        public int Expo { get; set; }

        public Member(double coefficient, int exponent)
        {
            this.Coeff = coefficient;
            this.Expo = exponent;
        }

        public Member Multiply(Member member)
        {
            return new Member(Coeff * member.Coeff, Expo + member.Expo);
        }
    }

}