INSERT INTO TipNamestaja(Naziv,Obrisan) 
VALUES('Sofa',0);
INSERT INTO TipNamestaja(Naziv,Obrisan) 
VALUES('Ormani',0);

INSERT INTO Namestaj(Naziv,Kolicina,Sifra,Tip_Namestaja,Cena,AkcijskaCena,Obrisan)
VALUES ('Super sofa',100,'SF124',1,15000,0,0);
INSERT INTO Namestaj(Naziv,Kolicina,Sifra,Tip_Namestaja,Cena,AkcijskaCena,Obrisan)
VALUES ('Orman delux',150,'OD128',2,10000,0,0);

INSERT INTO DodatneUsluge(Naziv,Cena,Obrisan)
VALUES('Dostava',5000,0);
INSERT INTO DodatneUsluge(Naziv,Cena,Obrisan)
VALUES('Montaza',3500,0);

INSERT INTO Korisnik(Ime,Prezime,Korisnicko_Ime,Lozinka,Tip_Korisnika,Obrisan)
VALUES('a','a','a','a','Administrator',0);
INSERT INTO Korisnik(Ime,Prezime,Korisnicko_Ime,Lozinka,Tip_Korisnika,Obrisan)
VALUES('b','b','b','b','Prodavac',0);

INSERT INTO Akcija(Datum_Pocetka,Datum_Kraja,Popust,Obrisan)
VALUES('12/23/2017 12:35:44','01/14/2018 15:14:13',15,0);

INSERT INTO NaAkciji(NamestajId,AkcijaId,Obrisan)
VALUES(1,1,0);

INSERT INTO Prodaja(Kupac,Broj_Racuna,Datum_Prodaje,Ukupan_Iznos,Obrisan)
VALUES('Marko Markovic',001,'12/20/2017',20000,0);

INSERT INTO Stavka(Kolicina,Cena,NamestajId,ProdajaId,Obrisan)
VALUES(1,15000,1,1,0);

INSERT INTO ProdateUsluge(UslugeId,ProdajaId,Obrisan)
VALUES(1,1,0);

INSERT INTO Salon (Naziv,Adresa,Broj_telefona,Email,Adresa_sajta,PIB,Maticni_broj,Broj_ziro_racuna) 
VALUES('FTN ideale','Trg Dositeja Obradovica 5,Novi Sad','015-339-4787','ftnideale@gmail.com','www.ftnideale.com','106071458',1587,'810-25-1-1687')