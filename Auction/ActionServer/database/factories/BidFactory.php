<?php
/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 5/2/2017
 * Time: 1:50 PM
 */

use App\Entities\Bid;


$factory->define(Bid::class, function (Faker\Generator $faker) {
    $user_id = $faker->numberBetween(1, config('factory.USER_AMOUNT'));
    $item_id = $faker->numberBetween(1, config('factory.ITEM_AMOUNT'));
    return [
        'bid_amount' => $faker->numberBetween(100000, 100000000),
        'time' => $faker->dateTimeBetween($startDate = '-10 days', $endDate = 'now'),
        'user_id' => $user_id,
        'item_id' => $item_id
    ];
});