using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Gol
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void EtantDonneUneCelluleEnvieAvecUnVoisinEnVieQuandElleMuteElleMeurt()
        {
            var celluleEnVie = new Cell();
            var autreCelluleEnVie = new Cell();

            celluleEnVie.AjouterVoisin(autreCelluleEnVie);
            celluleEnVie.Muter();

            Assert.IsFalse(celluleEnVie.EstEnVie());
        }

        [TestMethod]
        public void EtantDonneUneCelluleEnvieAvec2VoisinsEnVieQuandElleMuteElleVit()
        {
            var celluleEnVie = new Cell();
            celluleEnVie.AjouterVoisin(new Cell());
            celluleEnVie.AjouterVoisin(new Cell());
            
            celluleEnVie.Muter();

            Assert.IsTrue(celluleEnVie.EstEnVie());
        }

        [TestMethod]
        public void EtantDonneUneCelluleEnvieAvec3VoisinsEnVieQuandElleMuteElleVit()
        {
            var celluleEnVie = new Cell();
            celluleEnVie.AjouterVoisin(new Cell());
            celluleEnVie.AjouterVoisin(new Cell());
            celluleEnVie.AjouterVoisin(new Cell());

            celluleEnVie.Muter();

            Assert.IsTrue(celluleEnVie.EstEnVie());
        }

        [TestMethod]
        public void EtantDonneUneCelluleEnvieAvec2VoisinsMortQuandElleMuteElleMort()
        {
            var celluleEnVie = new Cell();
            celluleEnVie.AjouterVoisin(new Cell(CellEtat.Mort));
            celluleEnVie.AjouterVoisin(new Cell(CellEtat.Mort));

            celluleEnVie.Muter();

            Assert.IsFalse(celluleEnVie.EstEnVie());
        }

        [TestMethod]
        public void EtantDonneUneCelluleMorteAvec3VoisinsVivantsQuandElleMuteElleRevit()
        {
            var celluleEnVie = new Cell(CellEtat.Mort);
            celluleEnVie.AjouterVoisin(new Cell());
            celluleEnVie.AjouterVoisin(new Cell());
            celluleEnVie.AjouterVoisin(new Cell());

            celluleEnVie.Muter();

            Assert.IsTrue(celluleEnVie.EstEnVie()); 
        }
    }

    [TestClass]
    public class WorldTest
    {
        [TestMethod]
        public void EtantDonneUnUniversAvecUneCelluleVivanteEn01Et11E21AlorsLaCellule11ResteEnVie()
        {
            var univers = new Univers(new Position(0, 1), new Position(1, 1), new Position(2, 1));

            univers.Muter();

            Assert.IsTrue(univers.EstEnVie(new Position(1, 1)));
        }

        [TestMethod]
        [Ignore]
        public void EtantDonneUnUniversAvecUneCelluleVivanteEn11Et01E20AlorsLaCellule01Meurt()
        {
            var univers = new Univers(new Position(0, 1), new Position(1, 1), new Position(2, 0));

            univers.Muter();

            Assert.IsFalse(univers.EstEnVie(new Position(0, 1)));
        }
    }

    public class Univers
    {
        private Position[] _positions;

        public Univers(params Position[] positionsCelluleEnVie)
        {
            _positions = positionsCelluleEnVie.ToArray();
        }

        public void Muter()
        {

        }

        public bool EstEnVie(Position position)
        {            
            return _positions.Contains(position);
        }
    }

    public struct Position
    {
        public Position(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }
    }

    public enum CellEtat
    {
        EnVie,
        Mort
    }

    public class Cell
    {
        private List<Cell> _voisins;
        private CellEtat _etat;

        public Cell(CellEtat etat = CellEtat.EnVie)
        {
            _etat = etat;
            _voisins = new List<Cell>();
        }

        public void AjouterVoisin(Cell cell)
        {
            _voisins.Add(cell);
        }

        public void Muter()
        {
            var voisinsEnVie = _voisins.Count(v => v._etat == CellEtat.EnVie);
            if (voisinsEnVie == 2)
            {
                return;
            }

            if (voisinsEnVie == 3)
            {
                _etat = CellEtat.EnVie;
            }
            else
            {
                _etat = CellEtat.Mort;
            }
        }

        public bool EstEnVie()
        {            
            return _etat == CellEtat.EnVie;
        }
    }
}
