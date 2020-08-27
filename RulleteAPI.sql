CREATE DATABASE RuletteAPI;
USE RuletteAPI;

CREATE TABLE rulettes (
		idRulette			INTEGER IDENTITY(1,1) NOT NULL,
		ruletteStatus		VARCHAR(30),
		ruletteOpenDate		DATETIME,
		ruletteClosedDate	DATETIME

);
CREATE TABLE gamblers (
		idGambler			INTEGER IDENTITY(1,1) NOT NULL,
		nameGambler			VARCHAR(50) NOT NULL,
		cashGambler				NUMERIC(18,0),
		gamblerDate			DATETIME
);
CREATE TABLE bet (
		idBet				INTEGER IDENTITY(1,1) NOT NULL,
		cashBet				NUMERIC(18,0),
		colorBet			VARCHAR(20),
		numberBet			INT,
		idGambler			INTEGER NOT NULL,
		idRulette			INTEGER NOT NULL,
		betDate				DATETIME
);
ALTER TABLE rulettes ADD CONSTRAINT rulette_pk PRIMARY KEY ( idRulette );
ALTER TABLE gamblers ADD CONSTRAINT gamblers_pk PRIMARY KEY ( idGambler );
ALTER TABLE bet ADD CONSTRAINT bet_pk PRIMARY KEY ( idBet );
ALTER TABLE bet
    ADD CONSTRAINT bet_rulette_fk FOREIGN KEY ( idRulette )REFERENCES rulettes ( idRulette );
ALTER TABLE bet
    ADD CONSTRAINT bet_gamblers_fk FOREIGN KEY ( idGambler )REFERENCES gamblers ( idGambler );
