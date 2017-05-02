<?php
use App\Entities\User;
use Illuminate\Database\Seeder;

/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 5/2/2017
 * Time: 1:51 PM
 */
class UsersTableSeeder extends Seeder
{
    public function run() {
        factory(User::class, config('factory.USER_AMOUNT'))->create();
    }
}