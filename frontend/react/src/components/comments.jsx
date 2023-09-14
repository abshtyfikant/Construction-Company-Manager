import * as React from "react";
import "./comments.css";
import { json } from "react-router-dom";

export default function Comments({ serviceId, isOpen }) {
  const token = localStorage.getItem("token");
  const userId = localStorage.getItem("userId");
  const [comments, setComments] = React.useState("");
  const [fetchedWorkers, setFetchedWorkers] = React.useState();
  const [open, setOpen] = React.useState(isOpen);
  const contentRef = React.useRef("");
  const currDate = new Date().toJSON();

  const fetchData = React.useCallback(async () => {
    try {
      const response = await fetch("https://localhost:7098/api/Comment", {
        method: "get",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },
      });
      if (!response.ok) {
        throw new Error("Something went wrong!");
      }

      const data = await response.json();
      setComments(data);
    } catch (error) {
      //setError("Something went wrong, try again.");
    }

    try {
      const response = await fetch("https://localhost:7098/api/Employee", {
        method: "get",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },
      });

      if (!response.ok) {
        throw json(
          { message: "Could not fetch reports." },
          {
            status: 500,
          }
        );
      }

      const data = await response.json();
      setFetchedWorkers(data);
    } catch (error) {
      //setError("Something went wrong, try again.");
    }
  }, []);

  React.useEffect(() => {
    fetchData();
  }, [fetchData]);

  const handleAddComment = async (e) => {
    e.preventDefault();
    const commentData = {
      id: 0,
      serviceId: serviceId,
      userId: userId,
      content: contentRef.current?.value,
      date: currDate,
    };

    setComments([...comments, commentData]);

    const response = await fetch("https://localhost:7098/api/Comment", {
      method: "post",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
      body: JSON.stringify(commentData),
    });

    if (response.status === 422) {
      return response;
    }

    if (!response.ok) {
      throw json({ message: "Could not save comment." }, { status: 500 });
    }
    const data = await response.json();
    isOpen = false;
    return data;
  };

  const handlePopup = () => {
    if (isOpen) {
      return (
        <>
          <div className="comments__content">
            {comments.length > 0 ? (
              comments.map((comment) => {
                if (serviceId === comment.serviceId) {
                  return (
                    <div className="comment-content">
                      <div className="comment-content__header">
                        <p className="author">{comment.author}</p>
                        <p className="date">{comment.date.slice(0, 10)}</p>
                      </div>
                      <p className="content">{comment.content}</p>
                    </div>
                  );
                }
              })
            ) : (
              <p>No comments</p>
            )}
          </div>
          <div className="comment-input">
            <input
              id="new-comment"
              type="text"
              placeholder="Napisz komentarz..."
              ref={contentRef}
            />
            <button
              onClick={(e) => {
                handleAddComment(e);
              }}
            >
              Add comment
            </button>
          </div>
        </>
      );
    }
  };

  return (
    <div className={`comments-${isOpen ? "visible" : "hidden"}`}>
      {handlePopup()}
    </div>
  );
}
