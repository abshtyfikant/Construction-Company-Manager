import React from "react";
import { render, screen, fireEvent } from "@testing-library/react";
import { useNavigate } from "react-router-dom";
import Register from "./Register";

jest.mock("react-router-dom", () => ({
  useNavigate: jest.fn(),
}));

describe("Register", () => {
  it("renders register form", () => {
    render(<Register />);

    expect(screen.getByText("Zarejestruj się")).toBeInTheDocument();
    expect(screen.getByPlaceholderText("Imię")).toBeInTheDocument();
    expect(screen.getByPlaceholderText("Nazwisko")).toBeInTheDocument();
    expect(screen.getByPlaceholderText("Adres e-mail")).toBeInTheDocument();
    expect(screen.getByPlaceholderText("Hasło")).toBeInTheDocument();
    expect(screen.getByRole("button", { name: "Zaloguj" })).toBeInTheDocument();
  });

  it("registers successfully with valid data", () => {
    render(<Register />);
    const navigate = jest.fn();
    useNavigate.mockReturnValue(navigate);

    fireEvent.change(screen.getByPlaceholderText("Imię"), {
      target: { value: "John" },
    });
    fireEvent.change(screen.getByPlaceholderText("Nazwisko"), {
      target: { value: "Doe" },
    });
    fireEvent.change(screen.getByPlaceholderText("Adres e-mail"), {
      target: { value: "admin@example.com" },
    });
    fireEvent.change(screen.getByPlaceholderText("Hasło"), {
      target: { value: "admin123" },
    });
    fireEvent.click(screen.getByRole("button", { name: "Zaloguj" }));

    expect(navigate).toHaveBeenCalledWith("/menu");
    expect(console.log).toHaveBeenCalledWith("Zalogowano pomyślnie");
  });

  it("displays error message with invalid data", () => {
    render(<Register />);
    const alertMock = jest.spyOn(window, "alert").mockImplementation(() => {});

    fireEvent.change(screen.getByPlaceholderText("Adres e-mail"), {
      target: { value: "invalid-email" },
    });
    fireEvent.change(screen.getByPlaceholderText("Hasło"), {
      target: { value: "password" },
    });
    fireEvent.click(screen.getByRole("button", { name: "Zaloguj" }));

    expect(alertMock).toHaveBeenCalledWith("Błąd logowania");
    expect(console.log).toHaveBeenCalledWith("Błąd logowania");
  });
});
