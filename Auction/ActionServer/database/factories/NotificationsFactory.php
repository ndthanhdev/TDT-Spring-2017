<?php

/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 5/2/2017
 * Time: 11:12 PM
 */
$factory->define(App\Entities\Notification::class, function (Faker\Generator $faker) {
    return [
        'read' => $faker->boolean(),
        'type' => $faker->randomElement(['win', 'down']),
    ];
});