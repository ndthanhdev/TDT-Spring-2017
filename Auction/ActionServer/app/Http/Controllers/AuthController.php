<?php
/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 5/3/2017
 * Time: 10:32 AM
 */

namespace App\Http\Controllers;


use App\Entities\Item;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Hash;
use Tymon\JWTAuth\JWTAuth;

class AuthController extends Controller
{
    protected $jwt;

    /**
     * AuthController constructor.
     * @param $jwt
     */
    public function __construct(JWTAuth $jwt)
    {
        $this->jwt = $jwt;
    }

    public function login(Request $request)
    {

        if (!$token = $this->jwt->attempt($request->only('username', 'password'))) {
            return response()->json(['msg' => config('msg.LOGIN_FAILURE')], 401);
        }
        $user = Auth::user();

        $response = [
            'msg' => config('msg.LOGIN_SUCCESS'),
            'data' => ['token' => $token],
            'link' => [
                'name' => 'VIEW_USER',
//                'url' => route('users/{id}.GET', ['userId' => $user->id]),
                'method' => 'GET',
                'authentication' => true,
                'authorization' => 'USER'
            ]
        ];
        return response()->json($response, 200);
    }

    public function register(Request $request)
    {

    }

    public function test($id)
    {
        echo 'item_id'. $id;
        $item = Item::find($id)->first();
        echo 'user_id:'. $item->user_id;
//        $this->authorize('update', $item);
        return response()->json('this item is yrs');
    }

}