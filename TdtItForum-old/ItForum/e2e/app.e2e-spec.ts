import { ItForumPage } from './app.po';

describe('it-forum App', () => {
  let page: ItForumPage;

  beforeEach(() => {
    page = new ItForumPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
