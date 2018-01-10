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

INSERT INTO Salon (Naziv,Adresa,Broj_telefona,Email,Adresa_sajta,PIB,Maticni_broj,Broj_ziro_racuna) 
VALUES('FTN ideale','Trg Dositeja Obradovica 5,Novi Sad','015-339-4787','ftnideale@gmail.com','www.ftnideale.com','106071458',1587,'810-25-1-1687');