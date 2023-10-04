// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using DataAccessLayer;

Console.WriteLine("Hello, World!");

BenchmarkRunner.Run(typeof(Benchmarks));