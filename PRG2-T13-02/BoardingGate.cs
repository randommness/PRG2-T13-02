﻿namespace PRG2_T13_02
{
    public class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        public BoardingGate() { }
        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT, Flight flight)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT = supportsLWTT;
            Flight = flight;
        }

        public double CalculateFees()
        {
            return Flight.CalculateFees() + 300;
        }

        public override string ToString()
        {
            return $"Gate Name: {GateName}, Supports CFFT?: {SupportsCFFT}, " +
                   $"Supports DDJB?: {SupportsDDJB}, SupportsLWTT?: {SupportsLWTT}, Flight:\n{Flight}";
        }
    }
}
