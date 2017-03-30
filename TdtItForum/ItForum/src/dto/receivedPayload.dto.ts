/**
 * Created by duyth on 3/28/2017.
 */
export class ReceivedPayload {
  constructor(public data: JSON, public  message: String, public statusCode: number) {
  }
}
