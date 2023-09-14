import React from "react";
import { render, screen } from "@testing-library/react";
import { useParams } from "react-router-dom";
import ReportDetails from "./ReportDetails";

jest.mock("react-router-dom", () => ({
  useParams: jest.fn(),
}));

describe("ReportDetails", () => {
  it("displays loading message while fetching report details", () => {
    useParams.mockReturnValue({ reportId: "1" });
    render(<ReportDetails />);

    expect(screen.getByText("Ładowanie...")).toBeInTheDocument();
  });

  it("fetches and displays report details", async () => {
    const mockReport = {
      id: 1,
      reportType: "Report 1",
      beginDate: "2022-01-01",
      endDate: "2022-01-31",
      city: "Sample City",
      description: "Lorem ipsum dolor sit amet",
      amount: 1000,
      author: "John Doe",
    };
    useParams.mockReturnValue({ reportId: "1" });
    jest.spyOn(window, "fetch").mockResolvedValueOnce({
      ok: true,
      json: jest.fn().mockResolvedValueOnce(mockReport),
    });

    render(<ReportDetails />);

    expect(
      await screen.findByText("Typ raportu: Report 1")
    ).toBeInTheDocument();
    expect(screen.getByText("Data od: 2022-01-01")).toBeInTheDocument();
    expect(screen.getByText("Data do: 2022-01-31")).toBeInTheDocument();
    expect(screen.getByText("Miasto: Sample City")).toBeInTheDocument();
    expect(
      screen.getByText("Opis: Lorem ipsum dolor sit amet")
    ).toBeInTheDocument();
    expect(screen.getByText("Suma: 1000")).toBeInTheDocument();
    expect(
      screen.getByText("Wygenerowano przez: John Doe")
    ).toBeInTheDocument();
  });

  // Add more test cases as needed
});
