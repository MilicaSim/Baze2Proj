using Bioskop.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bioskop.ViewModel
{
    public class SjedisteViewModel:BindableBase
    {
        private bool isVisibleModifikuj;
        private bool isVisibleObrisi;
        private bool isVisibleDodaj;
        private bool isVisibleStek;

        private Sjediste sjedisteMD;
        private Sjediste selektovanoSjediste;
        public BindingList<Sjediste> sjedista;
        private BindingList<int> sale;


        public ICommand NavCommand { get; private set; }
        public ICommand DodajCommand { get; private set; }
        public ICommand ModifikujCommand { get; private set; }
        public ICommand ObrisiCommand { get; private set; }


        public SjedisteViewModel()
        {
            IsVisibleModifikuj = false;
            IsVisibleDodaj = false;
            IsVisibleObrisi = false;
            IsVisibleStek = false;

            NavCommand = new MyICommand<string>(OnNav);
            DodajCommand = new MyICommand(OnDodaj);
            ModifikujCommand = new MyICommand(OnModifikuj);
            ObrisiCommand = new MyICommand(OnObrisi);

            SjedisteMD = new Sjediste();
            SelektovanoSjediste = new Sjediste();
            Sjedista = GetAll();
            Sale = GetSale();
        }

        public BindingList<Sjediste> GetAll()
        {
            using (var access = new ModelContainer())
            {

                var vl = access.Sjedistes;
                BindingList<Sjediste> vs = new BindingList<Sjediste>();
                foreach (var v in vl)
                {
                    vs.Add(v);

                }
                return vs;
            }
        }
        public BindingList<int> GetSale()
        {
            using (var access = new ModelContainer())
            {

                var vl = access.Salas;
                BindingList<int> vs = new BindingList<int>();
                foreach (var v in vl)
                {
                    vs.Add(v.IdSale);

                }
                return vs;
            }
        }

        public void OnDodaj()
        {
            int brSjedista = 0;
            bool suc = false;
            using (var access = new ModelContainer())
            {
                try
                {
                    //if (SjedisteMD.SalaIdSale <= 0)
                    //{
                    //    MessageBox.Show("Mora biti odabrana sala!");
                    //    return;
                    //}
                    #region Validation
                    if (SjedisteMD.RedniBroj <=0)
                    {
                        MessageBox.Show("Broj sjedista mora biti popunjeno!");
                        return;
                    }
                    else if (SjedisteMD.Red<=0)
                    {
                        MessageBox.Show("Polje red mora biti popunjeno!");
                        return;
                    }
                    else
                    {
                    }
                    #endregion
                    SjedisteMD.Zauzeto = false;

                    access.Sjedistes.Add(SjedisteMD);
                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspijesno dodat objekat!");
                    }

                    SjedisteMD = new Sjediste();
                    Sjedista = GetAll();
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Sifra radnika vec postoji!");
                }

            }
        }
        public void OnModifikuj()
        {
            using (var access = new ModelContainer())
            {
                try
                {
                    access.Sjedistes.Where(n => n.IdSjedista == SelektovanoSjediste.IdSjedista).FirstOrDefault().RedniBroj = SjedisteMD.RedniBroj;
                    access.Sjedistes.Where(n => n.IdSjedista == SelektovanoSjediste.IdSjedista).FirstOrDefault().Red = SjedisteMD.Red;

                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspesno modifikovan objekat!");
                    }

                    Sjedista = GetAll();

                }
                catch (Exception)
                {
                    MessageBox.Show("Ne mozete modifikovati objekat jer niste odabrali objekat!");
                }
            }
        }
        public void OnObrisi()
        {
            using (var access = new ModelContainer())
            {
                try
                {
                    var sjediste = access.Sjedistes.FirstOrDefault(n => n.IdSjedista == SelektovanoSjediste.IdSjedista);
                    //access.Sadrzis.Remove(access.Sadrzis.FirstOrDefault(n => n.SjedisteIdSjedista == SelektovanoSjediste.IdSjedista));
                    sjediste.Sadrzis.Clear();
                    access.Sjedistes.Remove(sjediste);

                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspijesno obrisan objekat!");
                    }

                    Sjedista = GetAll();
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Ne mozete obrisati objekat jer niste odabrali objekat!");
                }
                catch (TargetException)
                {
                    MessageBox.Show("Ne mozete obrisati objekat jer niste odabrali objekat!");
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Ne mozete obrisati objekat jer je uvezan objekat!");
                }

            }
        }

        public void OnNav(object show)
        {

            string showView1 = (string)show;
            switch (showView1)
            {
                case "dodaj":
                    IsVisibleDodaj = true;
                    IsVisibleModifikuj = false;
                    IsVisibleObrisi = false;
                    IsVisibleStek = true;
                    SjedisteMD = new Sjediste();
                    break;
                case "obrisi":
                    IsVisibleDodaj = false;
                    IsVisibleModifikuj = false;
                    IsVisibleObrisi = true;
                    IsVisibleStek = false;
                    break;
                case "modifikuj":
                    IsVisibleDodaj = false;
                    IsVisibleModifikuj = true;
                    IsVisibleObrisi = false;
                    IsVisibleStek = true;
                    SjedisteMD = SelektovanoSjediste;
                    //if (GlumacMD != null)
                    //{
                    //    RadnikMD = GetAllRadnici().Where(n => n.IdRadnika == BlagajnikMD.IdRadnika).FirstOrDefault();
                    //}
                    //else
                    //{
                    //    RadnikMD = new Radnik();
                    //}

                    break;
            }
        }
        


        #region Property
        public Sjediste SjedisteMD
        {
            get { return sjedisteMD; }
            set
            {

                if (value != sjedisteMD)
                {
                    sjedisteMD = value;
                    OnPropertyChanged("SjedisteMD");
                }
            }
        }
        public Sjediste SelektovanoSjediste
        {
            get { return selektovanoSjediste; }
            set
            {

                if (value != selektovanoSjediste)
                {
                    selektovanoSjediste = value;
                    OnPropertyChanged("SelektovanoSjediste");
                   
                }
            }
        }
        public BindingList<Sjediste> Sjedista
        {
            get { return sjedista; }
            set
            {

                if (value != sjedista)
                {
                    sjedista = value;
                    OnPropertyChanged("Sjedista");
                }
            }
        }


        public bool IsVisibleDodaj
        {
            get { return isVisibleDodaj; }
            set
            {
                isVisibleDodaj = value;
                OnPropertyChanged("IsVisibleDodaj");
            }
        }
        public bool IsVisibleStek
        {
            get { return isVisibleStek; }
            set
            {
                isVisibleStek = value;
                OnPropertyChanged("IsVisibleStek");
            }
        }
        public bool IsVisibleObrisi
        {
            get { return isVisibleObrisi; }
            set
            {
                isVisibleObrisi = value;
                OnPropertyChanged("IsVisibleObrisi");
            }
        }
        public bool IsVisibleModifikuj
        {
            get { return isVisibleModifikuj; }
            set
            {
                isVisibleModifikuj = value;
                OnPropertyChanged("IsVisibleModifikuj");
            }
        }

        public BindingList<int> Sale { get => sale; set => sale = value; }
        #endregion

    }
}
