import { TDTITForumPage } from './app.po';

describe('tdt-it-forum App', function() {
  let page: TDTITForumPage;

  beforeEach(() => {
    page = new TDTITForumPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
