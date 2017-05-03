<?php
/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 5/3/2017
 * Time: 3:28 PM
 */

namespace App\Http\Controllers;


use App\Entities\User;
use Illuminate\Support\Facades\Auth;

class UserController extends Controller
{
    public function show($id)
    {
        $user = User::find($id)->first();
        $user->password = '';
        return response()->json($user);
    }
}