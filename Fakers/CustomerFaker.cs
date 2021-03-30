﻿using Bogus;
using Microsoft.AspNetCore.Identity;
using Models;

namespace Fakers
{
    // dotnet add package Bogus
    // dotnet add package Microsoft.AspNetCore.Identity
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker(IPasswordHasher<Customer> passwordHasher)
        {
            UseSeed(1);
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Email, f => f.Person.Email);
            RuleFor(p => p.PhoneNumber, f => f.Person.Phone);
            RuleFor(p => p.DateOfBirth, f => f.Person.DateOfBirth);
            RuleFor(p => p.Username, f => f.Person.UserName);
            RuleFor(p => p.HashedPassword, (f, customer) => passwordHasher.HashPassword(customer, "12345"));
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));
        }
    }
}
