using System;

namespace DesignPatternsInPlausibleCode.Structural
{
    // example modified from https://www.dofactory.com/net/adapter-design-pattern
    // each class will get a comment, indicating what class it corresponds to in the UML class diagram of the pattern

    public static class Adapter
    {
        // acts as the Client
        public static void Run()
        {
            // Non-adapted chemical compound
            Compound unknown = new Compound("Unknown");
            unknown.Display();

            // Adapted chemical compounds
            Compound water = new RichCompound("Water");
            water.Display();

            Compound benzene = new RichCompound("Benzene");
            benzene.Display();

            Compound ethanol = new RichCompound("Ethanol");
            ethanol.Display();
        }
    }

    // acts as the Target
    class Compound
    {
        protected string _name;
        protected float _boilingPoint;
        protected float _meltingPoint;
        protected double _molecularWeight;
        protected string _molecularFormula;

        public Compound(string name)
        {
            _name = name;
        }

        public virtual void Display()
        {
            Console.WriteLine($"Compound: {_name}");
        }
    }

    // acts as the Adapter
    class RichCompound : Compound

    {
        private ChemicalDatabank _bank;

        public RichCompound(string name)
          : base(name)
        {
        }

        public override void Display()
        {
            _bank = new ChemicalDatabank();

            _boilingPoint = _bank.GetCriticalPoint(_name, "B");
            _meltingPoint = _bank.GetCriticalPoint(_name, "M");
            _molecularWeight = _bank.GetMolecularWeight(_name);
            _molecularFormula = _bank.GetMolecularStructure(_name);

            base.Display();
            Console.WriteLine(" Formula: {0}", _molecularFormula);
            Console.WriteLine(" Weight : {0}", _molecularWeight);
            Console.WriteLine(" Melting Pt: {0}", _meltingPoint);
            Console.WriteLine(" Boiling Pt: {0}", _boilingPoint);
        }
    }

    // acts as the Adaptee
    class ChemicalDatabank
    {
        public float GetCriticalPoint(string compound, string point)
        {
            // Melting Point
            if (point == "M")
            {
                return (compound.ToLower()) switch
                {
                    "water" => 0.0f,
                    "benzene" => 5.5f,
                    "ethanol" => -114.1f,
                    _ => 0f,
                };
            }
            // Boiling Point
            else
            {
                return (compound.ToLower()) switch
                {
                    "water" => 100.0f,
                    "benzene" => 80.1f,
                    "ethanol" => 78.3f,
                    _ => 0f,
                };
            }
        }

        public string GetMolecularStructure(string compound)
        {
            return (compound.ToLower()) switch
            {
                "water" => "H20",
                "benzene" => "C6H6",
                "ethanol" => "C2H5OH",
                _ => "",
            };
        }

        public double GetMolecularWeight(string compound)
        {
            return (compound.ToLower()) switch
            {
                "water" => 18.015,
                "benzene" => 78.1134,
                "ethanol" => 46.0688,
                _ => 0d,
            };
        }
    }
}
