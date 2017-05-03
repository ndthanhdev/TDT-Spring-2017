<?php

/*
|--------------------------------------------------------------------------
| Application Routes
|--------------------------------------------------------------------------
|
| Here is where you can register all of the routes for an application.
| It is a breeze. Simply tell Lumen the URIs it should respond to
| and give it the Closure to call when that URI is requested.
|
*/

$app->get('/', function () use ($app) {
    return $app->version();
});

$app->group(['prefix' => 'auth'], function () use ($app) {
    $app->post('login',['as' => 'auth.LOGIN','uses'=>'AuthController@login'] );
    $app->get('register', ['as' => 'auth.REGISTER','uses'=>'AuthController@register']);
});

$app->get('users/{id}',['as'=>'user.SHOW','uses'=>'UserController@show']);

$app->get('users/{id}/items',['as'=>'users/{id}/items.GET','uses'=>'ItemController@getItemsOfUser']);

$app->get('users/{id}/',['as'=>'users/{id}/items.GET','uses'=>'ItemController@getItemsOfUser']);

$app->get('test/{id}',['as'=>'test/{id}','uses'=>'AuthController@test']);