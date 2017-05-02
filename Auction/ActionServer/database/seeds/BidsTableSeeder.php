<?php
use App\Entities\Bid;
use App\Entities\Item;
use App\Entities\User;
use Illuminate\Database\Seeder;

/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 5/2/2017
 * Time: 10:30 PM
 */
class BidsTableSeeder extends Seeder
{
    public function run()
    {
        $faker = \Faker\Factory::create();
        $nobid = config('factory.BID_AMOUNT');
        for ($i = 0; $i < $nobid; $i++) {
            $item = $this->getRandomItem();
            $user = $this->getRandomUser();
            if ($item->bids->count() > 0) {
                $bid = $this->getHighestBid($item);
                $createdAt = $faker->dateTimeBetween($bid->created_at, $item->ending_time);
                factory(Bid::class)->create([
                    'bid_amount' => $faker->numberBetween(1, 1000) * 1000 + $bid->bid_amount,
                    'created_at' => $createdAt,
                    'updated_at' => $createdAt,
                    'user_id' => $user->id,
                    'item_id' => $item->id
                ]);

            } else {
                $createdAt = $faker->dateTimeThisMonth($max = $item->ending_time);
                factory(Bid::class)->create([
                    'bid_amount' => $item->starting_price,
                    'created_at' => $createdAt,
                    'updated_at' => $createdAt,
                    'user_id' => $user->id,
                    'item_id' => $item->id
                ]);
            }
        }
    }

    function getRandomUser()
    {
        $users = User::all();
        return $users[random_int(0, $users->count() - 1)];
    }

    function getRandomItem()
    {
        $items = Item::all();
        return $items[random_int(0, $items->count() - 1)];
    }

    function getHighestBid(Item $item)
    {
        return $item->bids()->orderBy('bid_amount', 'desc')->first();
    }
}