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
        Schema:
        create('users', function (Blueprint $table) {
            $table->increments('id');
            $table->string('email');
            $table->string('password');
            $table->date('dob');
            $table->string('full_name');
            $table->text('address');
        });

        create('items', function (Blueprint $table) {
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

        create('bids', function (Blueprint $table) {
            $table->increments('id');
            $table->double('price');
            $table->dateTime('time');

//            relations
            $table->unsignedInteger('user_id');
            $table->foreign('user_id')->references('id')->on('users');

            $table->unsignedInteger('item_id');
            $table->foreign('item_id')->references('id')->on('items');
        });

        create('notifications', function (Blueprint $table) {
            $table->increments('id');
            $table->text('content');
            $table->boolean('is_new');

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
        //
    }
}