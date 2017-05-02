<?php
use App\Entities\Item;
use App\Entities\Notification;
use Carbon\Carbon;
use Illuminate\Database\Seeder;

/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 5/2/2017
 * Time: 11:54 PM
 */
class NotificationsTableSeeder extends Seeder
{
    public function run()
    {
        $items = Item::all();
        foreach ($items as $item) {
            $bibs = $item->bids()->orderBy('bid_amount', 'asc')->get();
            $n = $bibs->count();
            if ($n > 0) {
                for ($i = 0; $i < $n - 1; $i++) {
                    $created_at = $bibs[$i + 1]->created_at; // notify when higher bid
                    factory(\App\Entities\Notification::class)->create([
                        'type' => 'down',
                        'user_id' => $bibs[$i]->user_id,
                        'item_id' => $bibs[$i]->item_id,
                        'created_at' => $created_at,
                        'updated_at' => $created_at

                    ]);
                }
                $carbonEndTime = Carbon::instance(new DateTime($item->ending_time));
                if (Carbon::now()->gt($carbonEndTime)) {
                    factory(Notification::class)->create([
                        'type' => 'win',
                        'user_id' => $bibs[$n - 1]->user_id,
                        'item_id' => $bibs[$i]->item_id,
                        'created_at' => $item->ending_time,
                        'updated_at' => $item->ending_time
                    ]);
                }
            }
        }
    }
}