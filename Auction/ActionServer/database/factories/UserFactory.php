<?php

use App\Entities\User;
use Illuminate\Support\Facades\Hash;

$factory->define(User::class, function (Faker\Generator $faker) {
    return [
        'username' => $faker->userName,
        'password' => Hash::make('123456'),
        'avatar' => $faker->imageUrl(500, 500, 'cats'),
        'email' => $faker->email,
        'full_name' => $faker->name,
        'dob' => $faker->dateTimeThisCentury($max = 'now', $timezone = date_default_timezone_get()),
        'address' => $faker->address
    ];
});