<?php

$factory->define(\App\Entities\Item::class, function (Faker\Generator $faker) {
    return [
        'name' => $faker->name(),
        'description' => $faker->text(),
        'image' => $faker->name(),
        'ending_time' => $faker->dateTimeBetween($startDate = 'now', $endDate = 'now + 2days'),
        'starting_price' => $faker->numberBetween(100000, 100000000),
        'user_id' => $faker->numberBetween(1, config('factory.USER_AMOUNT'))

    ];
});
