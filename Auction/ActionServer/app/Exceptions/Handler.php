<?php

namespace App\Exceptions;

use Exception;
use Illuminate\Auth\Access\AuthorizationException;
use Illuminate\Database\Eloquent\ModelNotFoundException;
use Illuminate\Validation\ValidationException;
use Laravel\Lumen\Exceptions\Handler as ExceptionHandler;
use Prettus\Validator\Exceptions\ValidatorException;
use Symfony\Component\HttpKernel\Exception\BadRequestHttpException;
use Symfony\Component\HttpKernel\Exception\HttpException;
use Symfony\Component\HttpKernel\Exception\MethodNotAllowedHttpException;
use Symfony\Component\HttpKernel\Exception\NotFoundHttpException;
use Tymon\JWTAuth\Exceptions\JWTException;
use Tymon\JWTAuth\Exceptions\TokenExpiredException;
use Tymon\JWTAuth\Exceptions\TokenInvalidException;

class Handler extends ExceptionHandler
{
    /**
     * A list of the exception types that should not be reported.
     *
     * @var array
     */
    protected $dontReport = [
        AuthorizationException::class,
        HttpException::class,
        ModelNotFoundException::class,
        ValidationException::class,
    ];

    /**
     * Report or log an exception.
     *
     * This is a great spot to send exceptions to Sentry, Bugsnag, etc.
     *
     * @param  \Exception $e
     * @return void
     */
    public function report(Exception $e)
    {
        parent::report($e);
    }

    /**
     * Render an exception into an HTTP response.
     *
     * @param  \Illuminate\Http\Request $request
     * @param  \Exception $e
     * @return \Illuminate\Http\Response
     */
    public function render($request, Exception $e)
    {
        if ($e instanceof ModelNotFoundException)
            return response()->json(['msg' => $e->getMessage()], 404);
        elseif ($e instanceof ValidatorException)
            return response()->json(['msg' => $e->getMessageBag()], 400);
        elseif ($e instanceof ValidationException)
            return response()->json(['msg' => $e->getMessage()], 400);
        elseif ($e instanceof NotFoundHttpException)
            return response()->json(['msg' => config('msg.API_NOT_SUPPORTED')], 404);
        elseif ($e instanceof BadRequestHttpException)
            return response()->json(['msg' => $e->getMessage()], 400);
        elseif ($e instanceof MethodNotAllowedHttpException)
            return response()->json(['msg' => config('msg.API_NOT_SUPPORTED')], 404);
        elseif ($e instanceof TokenExpiredException)
            return response()->json(['msg' => config('msg.TOKEN_EXPIRED')]);
        elseif ($e instanceof TokenInvalidException)
            return response()->json(['msg' => config('msg.TOKEN_INVALID')]);
        elseif ($e instanceof JWTException)
            return response()->json(['msg' => config('msg.TOKEN_ABSENT')]);
//        elseif ($e instanceof AuthorizationException)
//            return response()->json(['msg' => config('msg.PERMISSION_DENIED')], 403);

        return parent::render($request, $e);
    }
}
