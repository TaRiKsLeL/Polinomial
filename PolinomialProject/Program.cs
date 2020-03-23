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
            double time;

            IO io = new IO();
            int firstMaxExp = io.InputExponent();
            int secondMaxExp = io.InputExponent();

            //використовую аналог vector

            List<Member> membersLst = new List<Member>();
            Handler polinominalControllerLst = new Handler(membersLst);
            polinominalControllerLst.GenerateWithExp(firstMaxExp);
            io.ShowPolinominal(membersLst);

            List<Member> membersLst2 = new List<Member>();
            Handler polinominalControllerLst2 = new Handler(membersLst2);
            polinominalControllerLst2.GenerateWithExp(secondMaxExp);
            io.ShowPolinominal(membersLst2);


            Console.WriteLine("result List");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<Member> result = polinominalControllerLst.Multiply(polinominalControllerLst2);
            stopwatch.Stop();
            time = (stopwatch.Elapsed.TotalMilliseconds);

            io.ShowPolinominal(result);
            Console.WriteLine("spended time: " + time);

            //===========================================================================================================

            // виконую аналогічні дії з двосв'язним списком

            LinkedList<Member> membersLstLinked = new LinkedList<Member>();
            Handler polinominalControllerLstLinked = new Handler(membersLstLinked);
            polinominalControllerLstLinked.GenerateWithExp(firstMaxExp);
            io.ShowPolinominal(membersLstLinked);

            LinkedList<Member> membersLst2Linked = new LinkedList<Member>();
            Handler polinominalControllerLst2Linked = new Handler(membersLst2Linked);
            polinominalControllerLst2Linked.GenerateWithExp(secondMaxExp);
            io.ShowPolinominal(membersLst2Linked);


            Console.WriteLine("result LinkedList");

            stopwatch.Start();
            List<Member> resultLinked = polinominalControllerLst.Multiply(polinominalControllerLst2);
            stopwatch.Stop();
            time = (stopwatch.Elapsed.TotalMilliseconds);

            io.ShowPolinominal(resultLinked);
            Console.WriteLine("spended time: " + time);


        }
    }

    class IO
    {
        const string EXPONENT = "enter polinominal exponent:";
        public int InputExponent()
        {
            Console.WriteLine(EXPONENT);
            return Int32.Parse(Console.ReadLine());
        }

        public void ShowPolinominal(ICollection<Member> members)
        {
            foreach (Member member in members)
            {
                Console.WriteLine(member.Coeff + "x^" + member.Expo);
            }

            Console.WriteLine("==============================");
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
                members.Add(new Member(rndGenerator.Next(1, 100), i));
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