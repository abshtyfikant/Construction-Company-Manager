import { redirect } from 'react-router-dom';
import { action } from './logout';

describe('Logout', () => {
  it('removes token from local storage and redirects to homepage', () => {
    const removeItemSpy = jest.spyOn(localStorage, 'removeItem');
    const redirectSpy = jest.spyOn(redirect, 'default');

    action();

    expect(removeItemSpy).toHaveBeenCalledWith('token');
    expect(redirectSpy).toHaveBeenCalledWith('/');
  });
});
