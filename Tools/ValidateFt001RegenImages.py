#!/usr/bin/env python3
"""Validate regenerated FT001 line-by-line Event CG images.

Checks expected file membership, image dimensions, exact duplicate hashes,
simple perceptual hash similarity, and writes a JSON report plus contact sheet.
"""

from __future__ import annotations

import argparse
import datetime as _dt
import hashlib
import json
import math
import sys
from pathlib import Path
from statistics import median
from typing import Any

try:
    from PIL import Image, ImageDraw, ImageFont, UnidentifiedImageError
except ImportError as exc:
    print(
        "Pillow is required. Install it with: python -m pip install Pillow",
        file=sys.stderr,
    )
    raise SystemExit(2) from exc


PROJECT_ROOT = Path(__file__).resolve().parents[1]
DEFAULT_INPUT_DIR = (
    PROJECT_ROOT
    / "Assets"
    / "Resources"
    / "VN"
    / "EventCG"
    / "FT001_LineByLineLocked_Regen_20260611"
)
DEFAULT_OUTPUT_DIR = PROJECT_ROOT / "Docs" / "GeneratedSources" / "FT001_RegenValidation_20260611"
EXPECTED_SIZE = (1600, 900)

EXPECTED_FILENAMES = [
    "ft001_cg_t01_choice_idle.png",
    "ft001_cg_t01_l01_mother_neutral.png",
    "ft001_cg_t01_l02_child_anxious.png",
    "ft001_cg_t01_l03_mother_worried.png",
    "ft001_cg_t01_l04_child_quiet.png",
    "ft001_cg_t01_l05_teacher_concerned.png",
    "ft001_cg_t01_l06_supervisor_explaining.png",
    "ft001_cg_t01_reaction_a_mother_softened.png",
    "ft001_cg_t01_reaction_b_child_withdrawn.png",
    "ft001_cg_t01_reaction_c_teacher_procedural.png",
    "ft001_cg_t02_choice_idle.png",
    "ft001_cg_t02_l01_mother_defensive.png",
    "ft001_cg_t02_l02_child_quiet.png",
    "ft001_cg_t02_l03_mother_exhausted.png",
    "ft001_cg_t02_l04_child_hesitant.png",
    "ft001_cg_t02_l05_teacher_procedural.png",
    "ft001_cg_t02_l06_supervisor_explaining.png",
    "ft001_cg_t02_reaction_a_mother_softened.png",
    "ft001_cg_t02_reaction_b_mother_defensive.png",
    "ft001_cg_t02_reaction_c_child_withdrawn.png",
    "ft001_cg_t03_choice_idle.png",
    "ft001_cg_t03_l01_grandmother_critical.png",
    "ft001_cg_t03_l02_mother_exhausted.png",
    "ft001_cg_t03_l03_grandmother_worried.png",
    "ft001_cg_t03_l04_mother_tearful.png",
    "ft001_cg_t03_l05_child_scared.png",
    "ft001_cg_t03_l06_supervisor_questioning.png",
    "ft001_cg_t03_reaction_a_grandmother_softened.png",
    "ft001_cg_t03_reaction_b_grandmother_defensive.png",
    "ft001_cg_t03_reaction_c_child_hesitant.png",
    "ft001_cg_t04_choice_idle.png",
    "ft001_cg_t04_l01_supervisor_questioning.png",
    "ft001_cg_t04_l02_mother_worried.png",
    "ft001_cg_t04_l03_child_quiet.png",
    "ft001_cg_t04_l04_mother_listening.png",
    "ft001_cg_t04_l05_child_hesitant.png",
    "ft001_cg_t04_l06_supervisor_explaining.png",
    "ft001_cg_t04_reaction_a_supervisor_approving.png",
    "ft001_cg_t04_reaction_b_mother_anxious.png",
    "ft001_cg_t04_reaction_c_child_withdrawn.png",
    "ft001_cg_t05_choice_idle.png",
    "ft001_cg_t05_l01_teacher_concerned.png",
    "ft001_cg_t05_l02_mother_softened.png",
    "ft001_cg_t05_l03_child_relieved.png",
    "ft001_cg_t05_l04_grandmother_softened.png",
    "ft001_cg_t05_l05_teacher_softened.png",
    "ft001_cg_t05_l06_supervisor_reflective.png",
    "ft001_cg_t05_reaction_a_mother_softened.png",
    "ft001_cg_t05_reaction_b_child_scared.png",
    "ft001_cg_t05_reaction_c_teacher_procedural.png",
    "ft001_cg_intro_01_mother_neutral.png",
    "ft001_cg_intro_02_child_neutral.png",
    "ft001_cg_intro_03_grandmother_neutral.png",
    "ft001_cg_intro_04_teacher_neutral.png",
    "ft001_cg_intro_05_supervisor_explaining.png",
    "ft001_cg_t02_l00_branch_mother_open.png",
    "ft001_cg_t02_l00_branch_child_closed.png",
    "ft001_cg_t02_l00_branch_teacher_cautious.png",
    "ft001_cg_t03_l00_branch_child_links_pattern.png",
    "ft001_cg_t03_l00_branch_mother_defensive.png",
    "ft001_cg_t03_l00_branch_mother_cautious.png",
    "ft001_cg_t04_l00_branch_grandmother_softened.png",
    "ft001_cg_t04_l00_branch_grandmother_stubborn.png",
    "ft001_cg_t04_l00_branch_child_exception.png",
    "ft001_cg_t05_l00_branch_teacher_adjusts.png",
    "ft001_cg_t05_l00_branch_child_scared.png",
    "ft001_cg_t05_l00_branch_mother_anxious.png",
]


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(
        description="Validate the FT001 regenerated 67-image Event CG set.",
    )
    parser.add_argument(
        "--input",
        type=Path,
        default=DEFAULT_INPUT_DIR,
        help=f"Folder containing regenerated PNGs. Default: {DEFAULT_INPUT_DIR}",
    )
    parser.add_argument(
        "--output-dir",
        type=Path,
        default=DEFAULT_OUTPUT_DIR,
        help=f"Directory for generated report/contact sheet. Default: {DEFAULT_OUTPUT_DIR}",
    )
    parser.add_argument(
        "--report",
        type=Path,
        default=None,
        help="Explicit JSON report path. Defaults to a timestamped file under --output-dir.",
    )
    parser.add_argument(
        "--contact-sheet",
        type=Path,
        default=None,
        help="Explicit contact sheet PNG path. Defaults to a timestamped file under --output-dir.",
    )
    parser.add_argument(
        "--no-contact-sheet",
        action="store_true",
        help="Skip contact sheet generation.",
    )
    parser.add_argument(
        "--similarity-threshold",
        type=int,
        default=4,
        help="Flag pHash pairs with Hamming distance <= this value. Default: 4.",
    )
    parser.add_argument(
        "--closest-pairs",
        type=int,
        default=20,
        help="Number of closest pHash pairs to keep in the report. Default: 20.",
    )
    parser.add_argument(
        "--fail-on-similar",
        action="store_true",
        help="Fail the run when pHash pairs are found at or below --similarity-threshold.",
    )
    parser.add_argument(
        "--overwrite",
        action="store_true",
        help="Allow overwriting explicit --report or --contact-sheet paths.",
    )
    return parser.parse_args()


def sha256_file(path: Path) -> str:
    digest = hashlib.sha256()
    with path.open("rb") as handle:
        for chunk in iter(lambda: handle.read(1024 * 1024), b""):
            digest.update(chunk)
    return digest.hexdigest()


def phash_64(path: Path) -> str:
    """Return a simple 64-bit DCT perceptual hash as a hex string."""
    resampling = getattr(Image, "Resampling", Image).LANCZOS
    with Image.open(path) as image:
        gray = image.convert("L").resize((32, 32), resampling)
    pixels = list(gray.getdata())
    matrix = [pixels[row * 32 : (row + 1) * 32] for row in range(32)]

    coeffs: list[float] = []
    for u in range(8):
        cu = 1 / math.sqrt(2) if u == 0 else 1
        for v in range(8):
            cv = 1 / math.sqrt(2) if v == 0 else 1
            total = 0.0
            for x in range(32):
                cos_x = math.cos(((2 * x + 1) * u * math.pi) / 64)
                for y in range(32):
                    total += (
                        matrix[x][y]
                        * cos_x
                        * math.cos(((2 * y + 1) * v * math.pi) / 64)
                    )
            coeffs.append(0.25 * cu * cv * total)

    threshold = median(coeffs[1:])
    bits = 0
    for coeff in coeffs:
        bits = (bits << 1) | int(coeff > threshold)
    return f"{bits:016x}"


def hamming_hex(left: str, right: str) -> int:
    return (int(left, 16) ^ int(right, 16)).bit_count()


def ensure_write_path(path: Path, overwrite: bool) -> None:
    if path.exists() and not overwrite:
        raise FileExistsError(f"Refusing to overwrite existing file: {path}")
    path.parent.mkdir(parents=True, exist_ok=True)


def inspect_expected_files(input_dir: Path) -> tuple[list[dict[str, Any]], list[dict[str, str]]]:
    records: list[dict[str, Any]] = []
    read_errors: list[dict[str, str]] = []
    for expected_name in EXPECTED_FILENAMES:
        path = input_dir / expected_name
        record: dict[str, Any] = {
            "name": expected_name,
            "path": str(path),
            "exists": path.exists(),
            "sha256": None,
            "phash64": None,
            "width": None,
            "height": None,
            "dimension_ok": False,
            "read_error": None,
        }
        if path.exists():
            try:
                with Image.open(path) as image:
                    width, height = image.size
                    image.verify()
                record["width"] = width
                record["height"] = height
                record["dimension_ok"] = (width, height) == EXPECTED_SIZE
                record["sha256"] = sha256_file(path)
                record["phash64"] = phash_64(path)
            except (OSError, UnidentifiedImageError) as exc:
                message = str(exc)
                record["read_error"] = message
                read_errors.append({"name": expected_name, "error": message})
        records.append(record)
    return records, read_errors


def find_exact_duplicates(records: list[dict[str, Any]]) -> list[dict[str, Any]]:
    by_hash: dict[str, list[str]] = {}
    for record in records:
        digest = record.get("sha256")
        if digest:
            by_hash.setdefault(digest, []).append(record["name"])
    return [
        {"sha256": digest, "files": names}
        for digest, names in sorted(by_hash.items())
        if len(names) > 1
    ]


def find_phash_pairs(
    records: list[dict[str, Any]],
    threshold: int,
    closest_count: int,
) -> tuple[list[dict[str, Any]], list[dict[str, Any]]]:
    hashed = [(record["name"], record["phash64"]) for record in records if record.get("phash64")]
    all_pairs: list[dict[str, Any]] = []
    flagged: list[dict[str, Any]] = []
    for index, (left_name, left_hash) in enumerate(hashed):
        for right_name, right_hash in hashed[index + 1 :]:
            distance = hamming_hex(left_hash, right_hash)
            pair = {
                "left": left_name,
                "right": right_name,
                "hamming_distance": distance,
                "left_phash64": left_hash,
                "right_phash64": right_hash,
            }
            all_pairs.append(pair)
            if distance <= threshold:
                flagged.append(pair)
    all_pairs.sort(key=lambda item: (item["hamming_distance"], item["left"], item["right"]))
    flagged.sort(key=lambda item: (item["hamming_distance"], item["left"], item["right"]))
    return flagged, all_pairs[: max(0, closest_count)]


def draw_wrapped_text(draw: ImageDraw.ImageDraw, xy: tuple[int, int], text: str, font: ImageFont.ImageFont, fill: tuple[int, int, int], max_chars: int) -> None:
    lines = []
    current = ""
    for part in text.replace("_", "_ ").split(" "):
        candidate = current + part
        if len(candidate) > max_chars and current:
            lines.append(current.rstrip())
            current = part
        else:
            current = candidate
    if current:
        lines.append(current.rstrip())
    x, y = xy
    for line in lines[:3]:
        draw.text((x, y), line, font=font, fill=fill)
        y += 12


def make_contact_sheet(
    input_dir: Path,
    records: list[dict[str, Any]],
    extra_names: list[str],
    output_path: Path,
) -> None:
    tile_w = 320
    thumb_h = 180
    label_h = 54
    pad = 14
    cols = 5
    names = [record["name"] for record in records] + extra_names
    rows = max(1, math.ceil(len(names) / cols))
    sheet_w = cols * tile_w + (cols + 1) * pad
    sheet_h = rows * (thumb_h + label_h) + (rows + 1) * pad
    sheet = Image.new("RGB", (sheet_w, sheet_h), (245, 245, 242))
    draw = ImageDraw.Draw(sheet)
    font = ImageFont.load_default()
    record_by_name = {record["name"]: record for record in records}
    resampling = getattr(Image, "Resampling", Image).LANCZOS

    for index, name in enumerate(names):
        col = index % cols
        row = index // cols
        x = pad + col * (tile_w + pad)
        y = pad + row * (thumb_h + label_h + pad)
        path = input_dir / name
        record = record_by_name.get(name)
        is_extra = name in extra_names
        is_missing = record is not None and not record["exists"]
        is_bad_dimension = record is not None and record["exists"] and not record["dimension_ok"]
        border = (80, 80, 80)
        if is_missing or is_bad_dimension:
            border = (190, 40, 40)
        elif is_extra:
            border = (200, 120, 20)

        draw.rectangle((x - 2, y - 2, x + tile_w + 1, y + thumb_h + label_h + 1), outline=border, width=3)
        if path.exists() and not is_missing:
            try:
                with Image.open(path) as image:
                    image = image.convert("RGB")
                    image.thumbnail((tile_w, thumb_h), resampling)
                    px = x + (tile_w - image.width) // 2
                    py = y + (thumb_h - image.height) // 2
                    sheet.paste(image, (px, py))
            except OSError:
                draw.rectangle((x, y, x + tile_w, y + thumb_h), fill=(55, 55, 55))
                draw.text((x + 10, y + 76), "UNREADABLE", font=font, fill=(255, 230, 230))
        else:
            draw.rectangle((x, y, x + tile_w, y + thumb_h), fill=(210, 210, 210))
            draw.text((x + 10, y + 76), "MISSING", font=font, fill=(120, 40, 40))

        label_y = y + thumb_h + 6
        prefix = f"{index + 1:02d}. "
        if is_extra:
            prefix = "EXTRA. "
        draw_wrapped_text(draw, (x + 6, label_y), prefix + name, font, (20, 20, 20), 42)
        if record and record["width"] and record["height"]:
            dim_text = f"{record['width']}x{record['height']}"
            dim_fill = (30, 90, 45) if record["dimension_ok"] else (160, 30, 30)
            draw.text((x + 6, label_y + 38), dim_text, font=font, fill=dim_fill)

    sheet.save(output_path)


def build_report(args: argparse.Namespace) -> tuple[dict[str, Any], Path | None]:
    input_dir = args.input.resolve()
    actual_png_names = sorted(path.name for path in input_dir.glob("*.png")) if input_dir.exists() else []
    expected_set = set(EXPECTED_FILENAMES)
    actual_set = set(actual_png_names)
    missing_names = sorted(expected_set - actual_set)
    extra_names = sorted(actual_set - expected_set)
    records, read_errors = inspect_expected_files(input_dir) if input_dir.exists() else ([], [])
    bad_dimensions = [
        {
            "name": record["name"],
            "width": record["width"],
            "height": record["height"],
            "expected_width": EXPECTED_SIZE[0],
            "expected_height": EXPECTED_SIZE[1],
        }
        for record in records
        if record["exists"] and not record["dimension_ok"] and not record["read_error"]
    ]
    exact_duplicates = find_exact_duplicates(records)
    similar_pairs, closest_pairs = find_phash_pairs(
        records,
        args.similarity_threshold,
        args.closest_pairs,
    )

    timestamp = _dt.datetime.now().astimezone().isoformat(timespec="seconds")
    fail_reasons = []
    if not input_dir.exists():
        fail_reasons.append("input_dir_missing")
    if len(actual_png_names) != len(EXPECTED_FILENAMES):
        fail_reasons.append("png_count_not_67")
    if missing_names:
        fail_reasons.append("missing_expected_files")
    if extra_names:
        fail_reasons.append("extra_png_files")
    if read_errors:
        fail_reasons.append("unreadable_png_files")
    if bad_dimensions:
        fail_reasons.append("bad_dimensions")
    if exact_duplicates:
        fail_reasons.append("exact_sha256_duplicates")
    if similar_pairs and args.fail_on_similar:
        fail_reasons.append("similar_phash_pairs")

    report: dict[str, Any] = {
        "schema": "ft001_regen_image_validation.v1",
        "generated_at": timestamp,
        "input_dir": str(input_dir),
        "expected_png_count": len(EXPECTED_FILENAMES),
        "actual_png_count": len(actual_png_names),
        "expected_size": {"width": EXPECTED_SIZE[0], "height": EXPECTED_SIZE[1]},
        "similarity_threshold_hamming": args.similarity_threshold,
        "fail_on_similar": bool(args.fail_on_similar),
        "status": "pass" if not fail_reasons else "fail",
        "fail_reasons": fail_reasons,
        "checks": {
            "input_dir_exists": input_dir.exists(),
            "png_count_is_67": len(actual_png_names) == len(EXPECTED_FILENAMES),
            "no_missing_expected_files": not missing_names,
            "no_extra_png_files": not extra_names,
            "all_png_readable": not read_errors,
            "all_dimensions_1600x900": not bad_dimensions,
            "no_exact_sha256_duplicates": not exact_duplicates,
            "no_similar_phash_pairs_at_threshold": not similar_pairs,
        },
        "missing": missing_names,
        "extra": extra_names,
        "bad_dimensions": bad_dimensions,
        "read_errors": read_errors,
        "exact_duplicates": exact_duplicates,
        "similar_pairs": similar_pairs,
        "closest_phash_pairs": closest_pairs,
        "files": records,
    }

    contact_path: Path | None = None
    if not args.no_contact_sheet:
        contact_path = args.contact_sheet
        if contact_path is None:
            stamp = _dt.datetime.now().strftime("%Y%m%d_%H%M%S")
            contact_path = args.output_dir / f"ft001_regen_contact_sheet_{stamp}.png"
        contact_path = contact_path.resolve()
        ensure_write_path(contact_path, args.overwrite)
        if input_dir.exists():
            make_contact_sheet(input_dir, records, extra_names, contact_path)
            report["contact_sheet"] = str(contact_path)
        else:
            report["contact_sheet"] = None
    else:
        report["contact_sheet"] = None

    return report, contact_path


def main() -> int:
    args = parse_args()
    report_path = args.report
    if report_path is None:
        stamp = _dt.datetime.now().strftime("%Y%m%d_%H%M%S")
        report_path = args.output_dir / f"ft001_regen_validation_report_{stamp}.json"
    report_path = report_path.resolve()

    try:
        ensure_write_path(report_path, args.overwrite)
        report, _ = build_report(args)
        report_path.write_text(json.dumps(report, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    except FileExistsError as exc:
        print(str(exc), file=sys.stderr)
        return 2

    print(f"FT001 regen validation: {report['status'].upper()}")
    print(f"Input: {report['input_dir']}")
    print(f"PNG count: {report['actual_png_count']} / {report['expected_png_count']}")
    print(f"Report: {report_path}")
    if report.get("contact_sheet"):
        print(f"Contact sheet: {report['contact_sheet']}")
    if report["fail_reasons"]:
        print("Fail reasons: " + ", ".join(report["fail_reasons"]))
    return 0 if report["status"] == "pass" else 1


if __name__ == "__main__":
    raise SystemExit(main())
