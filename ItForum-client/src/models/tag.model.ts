/**
 * Created by duyth on 4/18/2017.
 */
export class Tag {
  public tagId: string;
  public name: string;
  public description: string;

  constructor(tagId: string, name: string, description: string) {
    this.tagId = tagId;
    this.name = name;
    this.description = description;
  }
}
