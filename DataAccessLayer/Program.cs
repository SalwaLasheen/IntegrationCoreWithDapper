// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using DataAccessLayer;
using EFCore2VSDapper;

Console.WriteLine("Hello, World!");


BenchmarkRunner.Run(typeof(Benchmarks));

//BenchmarkRunner.Run(typeof(EFCore2VsDapperBenchMarks));


