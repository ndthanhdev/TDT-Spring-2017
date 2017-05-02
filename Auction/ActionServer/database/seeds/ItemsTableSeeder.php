<?php
use App\Entities\Item;
use Illuminate\Database\Seeder;

/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 5/2/2017
 * Time: 2:03 PM
 */
class ItemsTableSeeder extends Seeder
{
    public function run() {
        factory(Item::class, config('factory.ITEM_AMOUNT'))->create();
    }
}