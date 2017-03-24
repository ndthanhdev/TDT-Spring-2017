/**
 * Created by Duy Thanh Nguyen on 3/23/2017.
 */
export class ReceivedPayload {
  constructor(public data: JSON, public  message: String, public statusCode: number) {
  }
}
