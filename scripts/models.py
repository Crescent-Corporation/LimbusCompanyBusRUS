import msgspec
from pathlib import Path
import typing


class FontRule(msgspec.Struct):
    font: str
    path: str
    escape_short_keywords: bool = True
    escape_keywords: bool = True


class Font(msgspec.Struct):
    repo: str | None = None
    replacement_map_path: str | None = None
    font_path: str | None = None


class Reference(msgspec.Struct):
    path: str
    repo: str | None = None
    branch: str | None = None


class KeywordShorthands(msgspec.Struct):
    regex: str
    apply_for: list[str]


class Priority(msgspec.Struct):
    order: list[str] = msgspec.field(default_factory=list)


class XmlEscape(msgspec.Struct):
    singular_keywords: list[str] = msgspec.field(default_factory=list)


class Config(msgspec.Struct):
    font: Font
    reference: Reference
    font_rules: dict[str, list[FontRule]]
    keyword_shorthands: KeywordShorthands
    priority: Priority
    xml_escape: XmlEscape

    @staticmethod
    def from_file(path: Path) -> "Config":
        with open(path, "r") as f:
            content = f.read()
        return msgspec.toml.decode(content, type=Config)


class ReleaseAsset(typing.TypedDict):
    name: str
    browser_download_url: str
