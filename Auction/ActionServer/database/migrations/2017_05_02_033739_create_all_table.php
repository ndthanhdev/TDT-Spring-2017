<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateAllTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('users', function (Blueprint $table) {
            $table->increments('id');
            $table->string('username');
            $table->string('password');
            $table->string('email');
            $table->date('dob');
            $table->string('full_name');
            $table->text('address');
            $table->text('avatar');
        });

        Schema::create('items', function (Blueprint $table) {
            $table->increments('id');
            $table->string('name');
            $table->string('description');
            $table->text('image');
            $table->dateTime('ending_time');
            $table->double('starting_price');

//            relations
            $table->unsignedInteger('user_id');
            $table->foreign('user_id')->references('id')->on('users');
        });

        Schema::create('bids', function (Blueprint $table) {
            $table->increments('id');
            $table->double('bid_amount');
            $table->timestamps();

//            relations
            $table->unsignedInteger('user_id');
            $table->foreign('user_id')->references('id')->on('users');

            $table->unsignedInteger('item_id');
            $table->foreign('item_id')->references('id')->on('items');
        });

        Schema::create('notifications', function (Blueprint $table) {
            $table->increments('id');
            $table->boolean('read');
            $table->enum('type',['win','down']);
            $table->timestamps();

//            relations
            $table->unsignedInteger('user_id');
            $table->foreign('user_id')->references('id')->on('users');

            $table->unsignedInteger('item_id');
            $table->foreign('item_id')->references('id')->on('items');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('notifications');
        Schema::dropIfExists('bids');
        Schema::dropIfExists('items');
        Schema::dropIfExists('users');
    }
}
